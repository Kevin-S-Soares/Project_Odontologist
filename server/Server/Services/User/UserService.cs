using Server.Contexts;
using Server.Models.Client;
using Server.Models;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Services;

public class UserService(ApplicationContext context, IEmailService _emailService) : IUserService
{
    private readonly ApplicationContext _context = context;

    public async Task<ServiceResponse<User>> CreateAsync(ClientUser request)
    {
        var query = _context.Users.FirstOrDefault(search => search.Email == request.Email);
        if (query is not null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Email already registered."
            };
        }

        var user = new User()
        {
            Name = request.Name,
            Email = request.Email,
            CreatedAt = DateTime.Now,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = (Role) new Random().Next(1, 4)
        };

        var storage = new HashStorage
        {
            Hash = GenerateNonRepetitiveHash(),
            Operation = Operation.REGISTER_ACCOUNT
        };

        try
        {
            await _context.Users.AddAsync(user);
            storage.UserId = user.Id;
            await _context.HashStorage.AddAsync(storage);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = $"Something went wrong while creating user: {e.Message}."
            };
        }

        await _emailService.ConfirmRegisterAsync(user, storage);

        user.Password = "";
        return new()
        {
            StatusCode = StatusCodes.Status201Created,
            Data = user
        };
    }

    public async Task<ServiceResponse<bool>> VerifyRegistrationAsync(ClientHashOperation request)
    {
        var query = _context.HashStorage.FirstOrDefault(search =>
            search.Hash == request.Hash && search.UserId == request.UserId
            && search.Operation == Operation.REGISTER_ACCOUNT
            && request.Operation == Operation.REGISTER_ACCOUNT);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Invalid hash, operation or user id."
            };
        }

        var user = _context.Users.First(search => search.Id == request.UserId);
        user.VerifiedAt = DateTime.Now;
        try
        {
            _context.Users.Update(user);
            _context.HashStorage.Remove(query);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = $"Something went wrong while verifying registration: {e.Message}"
            };
        }
        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = true
        };
    }

    public ServiceResponse<IEnumerable<User>> GetAll()
    {

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = [.. _context.Users.Select(search => new User 
            {
                Id = search.Id,
                Name = "",
                ProfilePictureUrl = search.ProfilePictureUrl,
                Email = "",
                Password = "",
                CreatedAt = search.CreatedAt,
                VerifiedAt = search.VerifiedAt,
                Role = search.Role
            })]
        };
    }

    public async Task<ServiceResponse<bool>> RemoveByIdAsync(Guid id)
    {
        var query = _context.Users.FirstOrDefault(search => search.Id == id);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = "User not found."
            };
        }


        try
        {
            _context.Users.Remove(query);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"Something went wrong while removing user: {e.Message}"
            };
        }

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = true
        };
    }

    public ServiceResponse<string> Authenticate(ClientAuthentication request)
    {
        var query = _context.Users.FirstOrDefault(search => search.Email == request.Email);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Password or password is wrong."
            };
        }

        var condition = BCrypt.Net.BCrypt.Verify(request.Password, query.Password);
        if (condition is false)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Password or password is wrong."
            };
        }


        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = CreateJWT(query)
        };
    }

    private string CreateJWT(User user)
    {
        string verified = user.VerifiedAt == null? "" : user.VerifiedAt.Value.ToString();
        var claims = new List<Claim>()
            {
                new("sub", user.Id.ToString()),
                new("name", user.Name),
                new("pic", user.ProfilePictureUrl),
                new("role", Convert.ToInt32(user.Role).ToString()),
                new("verified", verified)
            };
        /*
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            Environment.GetEnvironmentVariable("SECRET_KEY") ?? ""));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        */
        var rsa = RSA.Create();
        rsa.ImportFromPem(Environment.GetEnvironmentVariable("PRIVATE_KEY")?? "");
        var key = new RsaSecurityKey(rsa);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha512);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<ServiceResponse<bool>> ForgetPasswordAsync(string email)
    {
        var query = _context.Users.FirstOrDefault(search => search.Email == email);

        if(query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Invalid email."
            };
        }

        var condition = _context.HashStorage.Any(search => 
            search.UserId == query.Id && 
            search.Operation == Operation.RESET_PASSWORD && 
            DateTime.Now.Subtract(search.CreatedAt).Minutes < 15
            );
        if(condition is true){
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Wait 15 minutes to request a new password change."
            };
        }
        var hashStorage = new HashStorage{
            UserId = query.Id,
            Hash = GenerateNonRepetitiveHash(),
            Operation = Operation.RESET_PASSWORD
        };
        try
        {
            await _context.AddAsync(hashStorage);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = $"Something went wrong while forgetting password: {e.Message}"
            };
        }

        await _emailService.ForgetPasswordAsync(user: query, hashStorage);
        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = true
        };
    }

    public async Task<ServiceResponse<bool>> ResetPasswordAsync(ClientResetPassword request)
    {
        var query = _context.HashStorage.FirstOrDefault(search =>
            search.Hash == request.Hash && search.UserId == request.UserId
            && search.Operation == Operation.RESET_PASSWORD
            && request.Operation == Operation.RESET_PASSWORD);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Invalid hash, operation or user id."
            };
        }

        var user = _context.Users.First(search => search.Id == request.UserId);
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        try
        {
            _context.Users.Update(user);
            _context.HashStorage.Remove(query);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = $"Something went wrong while verifying registration: {e.Message}"
            };
        }
        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = true
        };
    }

    private string GenerateNonRepetitiveHash()
    {
        while (true)
        {
            var result = RandomNumberGenerator.GetHexString(64);
            bool condition = _context.HashStorage.Any(x => x.Hash == result);
            if (condition is false)
            {
                return result;
            }
        }
    }
}