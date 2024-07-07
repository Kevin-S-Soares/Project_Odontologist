using Server.Contexts;
using Server.Models.Client;
using Server.Models;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Server.Services;

public class UserService(ApplicationContext _context, IEmailService _emailService, IAuthService _authService) : IUserService
{
    public async Task<ServiceResponse<User>> CreateAsync(ClientRegisterUser request)
    {
        var query = _context.Users.FirstOrDefault(item => item.Email == request.Email);
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
            NormalizedName = request.Name.ToUpper(),
            CreatedAt = DateTime.Now,
            LastLogin = DateTime.Now,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = Role.VISITOR
        };

        var storage = new HashStorage
        {
            Hash = GenerateNonRepetitiveHash(),
            Operation = Operation.REGISTER_ACCOUNT,
            CreatedAt = DateTime.Now
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
        var query = _context.HashStorage.FirstOrDefault(item =>
            item.Hash == request.Hash && item.UserId == request.UserId
            && item.Operation == Operation.REGISTER_ACCOUNT
            && request.Operation == Operation.REGISTER_ACCOUNT);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Invalid hash, operation or user id."
            };
        }

        var user = _context.Users.First(item => item.Id == request.UserId);
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

    public ServiceResponse<IEnumerable<User>> GetAll(string? name)
    {
        var requester = _authService.GetRequester();
        if (requester is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = "User not found."
            };
        }
        if (requester.Role == Role.VISITOR)
        {
            return new()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = [.. _context.Users.Select(item => new User
            {
                Id = item.Id,
                Name = "",
                NormalizedName = "",
                ProfilePictureUrl = item.ProfilePictureUrl,
                Email = "",
                Password = "",
                LastLogin = item.LastLogin,
                CreatedAt = item.CreatedAt,
                VerifiedAt = item.VerifiedAt,
                Role = item.Role
            })]
            };
        }
        if (requester.Role == Role.ADMIN)
        {
            var aux = string.IsNullOrWhiteSpace(name) ? ".*" : name.ToUpper();
            var match = (User user) => Regex.Match(user.NormalizedName, aux).Success;
            return new()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = [.. _context.Users.ToArray().Where(item => match(item))]
            };
        }
        return new()
        {
            StatusCode = StatusCodes.Status403Forbidden,
            ErrorMessage = "Not authorized."
        };
    }

    public async Task<ServiceResponse<bool>> RemoveByIdAsync(Guid id)
    {
        var query = _context.Users.FirstOrDefault(item => item.Id == id);
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

    public async Task<ServiceResponse<string>> AuthenticateAsync(ClientAuthentication request)
    {
        var query = _context.Users.FirstOrDefault(item => item.Email == request.Email);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Email or password is wrong."
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

        query.LastLogin = DateTime.Now;
        try
        {
            _context.Users.Update(query);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = $"Something went wrong while authenticating user: {e.Message}"
            };
        }

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = CreateJWT(query)
        };
    }

    public async Task<ServiceResponse<bool>> ForgetPasswordAsync(string email)
    {
        var query = _context.Users.FirstOrDefault(item => item.Email == email);

        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Invalid email."
            };
        }

        var operation = _context.HashStorage.FirstOrDefault(item =>
            item.UserId == query.Id &&
            item.Operation == Operation.RESET_PASSWORD
            );
        var condition = operation is not null && DateTime.Now.Subtract(operation.CreatedAt).Minutes < 15;
        if (condition is true)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Wait 15 minutes to request a new password change."
            };
        }
        var hashStorage = new HashStorage
        {
            UserId = query.Id,
            Hash = GenerateNonRepetitiveHash(),
            Operation = Operation.RESET_PASSWORD,
            CreatedAt = DateTime.Now
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
        var query = _context.HashStorage.FirstOrDefault(item =>
            item.Hash == request.Hash && item.UserId == request.UserId
            && item.Operation == Operation.RESET_PASSWORD
            && request.Operation == Operation.RESET_PASSWORD);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Invalid hash, operation or user id."
            };
        }

        var user = _context.Users.First(item => item.Id == request.UserId);
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

    public async Task<ServiceResponse<bool>> ChangePasswordAsync(string request)
    {
        var user = _authService.GetRequester();
        if (user is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Id does not exist."
            };
        }
        var query = _context.Users.FirstOrDefault(item => item.Id == user.Id);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Id does not exist."
            };
        }
        query.Password = BCrypt.Net.BCrypt.HashPassword(request);
        try
        {
            _context.Users.Update(query);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = $"Something went wrong while changing password: {e.Message}"
            };
        }

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = true
        };

    }

    public ServiceResponse<User> GetUserById(Guid guid)
    {
        var query = _context.Users.FirstOrDefault(item => item.Id == guid);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = "User not found."
            };
        }

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = query
        };
    }


    public ServiceResponse<User> GetCurrentUser()
    {
        var requester = _authService.GetRequester();
        if (requester is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = "Requester does not exist."
            };
        }
        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = requester
        };
    }

    public ServiceResponse<IEnumerable<User>> GetOtherUsers(string? name)
    {
        var requester = _authService.GetRequester();
        if (requester is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = "Requester does not exist."
            };
        }
        if (requester.Role == Role.ADMIN)
        {
            var aux = string.IsNullOrWhiteSpace(name) ? ".*" : name.ToUpper();
            var match = (User user) => Regex.Match(user.NormalizedName, aux).Success;
            return new()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = [.. _context.Users.ToArray().Where(item => item.Id != requester.Id && match(item))]
            };
        }
        if (requester.Role == Role.VISITOR)
        {
            return new()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = [ .. _context.Users.Where( item => item.Id != requester.Id ).Select( item => new User()
                {
                    Id = item.Id,
                    Name = "",
                    NormalizedName = "",
                    ProfilePictureUrl = item.ProfilePictureUrl,
                    Email = "",
                    Password = "",
                    CreatedAt = item.CreatedAt,
                    LastLogin = item.LastLogin,
                    VerifiedAt = item.VerifiedAt,
                    Role = item.Role
                }) ]
            };
        }
        return new()
        {
            StatusCode = StatusCodes.Status403Forbidden,
            ErrorMessage = "Not authorized."
        };
    }

    public async Task<ServiceResponse<bool>> UpdateUserAsync(ClientUpdateUser request)
    {
        var requester = _authService.GetRequester();
        if (requester is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = "Requester does not exist."
            };
        }
        var query = _context.Users.FirstOrDefault(item => item.Id == request.Id);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = "User does not exist."
            };
        }

        if (requester.Role != Role.ADMIN && requester.Id != request.Id)
        {
            return new()
            {
                StatusCode = StatusCodes.Status403Forbidden,
                ErrorMessage = "Not authorized."
            };
        }

        if (request.Role == Role.ADMIN && requester.Role != Role.ADMIN)
        {
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "Cannot change role to administrator without being administrator."
            };
        }

        query.Name = request.Name;
        query.NormalizedName = request.Name.ToUpper();
        query.ProfilePictureUrl = request.ProfilePictureUrl;
        query.Role = request.Role;

        try
        {
            _context.Users.Update(query);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = $"Something went wrong while updating user: {e.Message}"
            };
        }

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = true
        };
    }

    public async Task<ServiceResponse<bool>> ChangeEmailAsync(string email)
    {
        var requester = _authService.GetRequester();
        if (requester is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Requester does not exist."
            };
        }

        var operation = _context.HashStorage.FirstOrDefault(item =>
            item.UserId == requester.Id &&
            item.Operation == Operation.CHANGE_EMAIL
            );
        var condition = operation is not null && DateTime.Now.Subtract(operation.CreatedAt).Minutes < 15;
        if (condition is true)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Wait 15 minutes to request a new email change."
            };
        }
        var hashStorage = new HashStorage
        {
            UserId = requester.Id,
            Hash = GenerateNonRepetitiveHash(),
            Operation = Operation.CHANGE_EMAIL,
            CreatedAt = DateTime.Now,
            Details = email
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

        await _emailService.ConfirmEmailAsync(user: requester, email, hashStorage);
        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = true
        };
    }

    public async Task<ServiceResponse<bool>> ConfirmEmailChangeAsync(ClientHashOperation request)
    {
        var query = _context.HashStorage.FirstOrDefault(item =>
            item.Hash == request.Hash && item.UserId == request.UserId
            && item.Operation == Operation.CHANGE_EMAIL
            && request.Operation == Operation.CHANGE_EMAIL);
        if (query is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status409Conflict,
                ErrorMessage = "Invalid hash, operation or user id."
            };
        }

        var user = _context.Users.First(item => item.Id == request.UserId);
        if (query.Details is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = "Something went wrong while confirming new email: Details are null."
            };
        }
        user.Email = query.Details;
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

    public async Task<ServiceResponse<bool>> ResetGuestAsync()
    {
        var guest = _context.Users.FirstOrDefault(item => item.Email == "guest@guest.com");
        if(guest is null)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = "Something went wrong while retrieving guest account!"
            };
        }

        guest.Name = "Guest";
        guest.NormalizedName = "GUEST";
        guest.Password = "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq";
        guest.Role = Role.VISITOR;
        guest.CreatedAt = DateTime.Now;
        guest.LastLogin = DateTime.Now;
        guest.VerifiedAt = DateTime.Now;

        try 
        {
            _context.Users.Update(guest);
            await _context.SaveChangesAsync();
        }
        catch(Exception e)
        {
            return new()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = $"Something went wrong while resetting guest: {e.Message}"
            };
        }

        return new() 
        {
            StatusCode = StatusCodes.Status200OK,
            Value = true
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

    private string CreateJWT(User user)
    {
        string verified = user.VerifiedAt == null ? "" : user.VerifiedAt.Value.ToString();
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
        rsa.ImportFromPem(Environment.GetEnvironmentVariable("PRIVATE_KEY") ?? "");
        var key = new RsaSecurityKey(rsa);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha512);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}