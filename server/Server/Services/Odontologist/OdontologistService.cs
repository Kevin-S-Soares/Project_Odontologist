using Server.Contexts;
using Server.Models;

namespace Server.Services
{
    public class OdontologistService : IOdontologistService
    {
        private readonly ApplicationContext _context;
        private readonly IAuthService _authService;

        public OdontologistService(ApplicationContext context,
            IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Odontologist>> CreateAsync(Odontologist odontologist)
        {
            if (_authService.IsAdmin())
            {
                try
                {
                    await _context.Odontologists.AddAsync(odontologist);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return new()
                    {
                        ErrorMessage = e.Message,
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                return new()
                {
                    Data = odontologist, 
                    StatusCode = StatusCodes.Status201Created
                };
            }
            return new()
            {
                ErrorMessage = "Not authorized",
                StatusCode = StatusCodes.Status403Forbidden
            };
        }

        public async Task<ServiceResponse<string>> DeleteAsync(long id)
        {
            var query = _context.Odontologists.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new()
                {
                    ErrorMessage =  "Odontologist does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            if (_authService.IsAdmin())
            {
                _context.Odontologists.Remove(query);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return new()
                    {
                        ErrorMessage = e.Message,
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                return new()
                {
                    Data = "Odontologist deleted", 
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new()
            {
                ErrorMessage = "Not authorized",
                StatusCode = StatusCodes.Status403Forbidden
            };
        }

        public ServiceResponse<IEnumerable<Odontologist>> FindAll()
        {
            if (_authService.IsAdmin() || _authService.IsAttendant())
            {
                var result = _context.Odontologists;
                return new()
                {
                    Data = result, 
                    StatusCode = StatusCodes.Status200OK
                };
            }
            if (_authService.IsOdontologist())
            {
                var odontologist = _context.Odontologists.Where(x => x.Id == _authService.GetContextID());
                return new()
                {
                    Data = odontologist,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new()
            {
                ErrorMessage = "Not authorized",
                StatusCode = StatusCodes.Status403Forbidden
            };
        }

        public ServiceResponse<Odontologist> FindById(long id)
        {
            var query = _context.Odontologists.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new()
                {
                    ErrorMessage ="Odontologist does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            if (_authService.IsAdmin() || _authService.IsAttendant()
                || (_authService.IsOdontologist() && IsOwner(query)))
            {
                return new()
                {
                    Data = query, 
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new()
            {
                ErrorMessage = "Not authorized",
                StatusCode = StatusCodes.Status403Forbidden
            };
        }

        public async Task<ServiceResponse<Odontologist>> UpdateAsync(Odontologist odontologist)
        {
            if (_authService.IsAdmin() || (_authService.IsOdontologist() && IsOwner(odontologist)))
            {
                bool condition = _context.Odontologists.Any(x => x.Id == odontologist.Id);
                if (condition is false)
                {
                    return new()
                    {
                        ErrorMessage = "Odontologist does not exist",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                try
                {
                    _context.Odontologists.Update(odontologist);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return new()
                    {
                        ErrorMessage = e.Message,
                        StatusCode =  StatusCodes.Status500InternalServerError
                    };
                }
                return new()
                {
                    Data = odontologist,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new()
            {
                ErrorMessage = "Not authorized",
                StatusCode = StatusCodes.Status403Forbidden
            };

        }

        private bool IsOwner(Odontologist odontologist)
        {
            return odontologist.Id == _authService.GetContextID();
        }
    }
}