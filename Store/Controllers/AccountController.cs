using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Context;
using Store.Dtos.UserDto;
using Store.Entities;
using Store.Interfaces;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly StoreDbContext _context;
        public AccountController(IAccountService accountService,StoreDbContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        [HttpGet]
        public Task<ICollection<User>> GetAllUsers()
        {
            return _accountService.GetUsers();
        }

        [HttpGet("{id:int}")]
        public async Task<GetSingleUser> GetUserId(int id)
        {
            GetSingleUser userDto = new GetSingleUser();
            try
            {
                if (id == 0)
                {
                    userDto.Status = "Id is empty";
                    userDto.User = null;
                    userDto.Code = 400;
                }
                else
                {
                    var user = await _accountService.GetUser(id);
                    if (user == null)
                    {
                        userDto.Status = "Not found";
                        userDto.User = null;
                        userDto.Code = 404;
                    }
                    else
                    {
                        userDto.Status = "Success";
                        userDto.User = user;
                        userDto.Code = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                userDto.User = null;
                userDto.Status = ex.InnerException.ToString();
                userDto.Code = 0;
            }

            return userDto;
        }
        [HttpPost("Login")]
        public async Task<GetSingleUser> Login(LoginDto loginDto)
        {
            GetSingleUser userDto = new GetSingleUser();
            try
            {
                if (loginDto == null)
                {
                    userDto.Status = "Id is empty";
                    userDto.User = null;
                    userDto.Code = 400;
                }
                else
                {
                    var user = await _accountService.Login(loginDto);
                    if (user == null)
                    {
                        userDto.Status = "Not found";
                        userDto.User = null;
                        userDto.Code = 404;
                    }
                    else
                    {
                        userDto.Status = "Success";
                        userDto.User = user;
                        userDto.Code = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                userDto.User = null;
                userDto.Status = ex.InnerException.ToString();
                userDto.Code = 0;
            }

            return userDto;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Registration(RegistrationDto registrationDto)
        {
            if (registrationDto == null)
            {
                return BadRequest("Invalid registration data");
            }

            try
            {
                var newUser = new User
                {
                    FirstName = registrationDto.FirstName,
                    LastName = registrationDto.LastName,
                    Email = registrationDto.Email,
                    Password = registrationDto.Password,
                    UserRoles = new List<UserRole>()
                };

                var role = await _context.Role.FirstOrDefaultAsync(x => x.Name.Contains("User"));

                if (role != null)
                {
                    var userRole = new UserRole
                    {
                        User = newUser,
                        Role = role
                    };

                    newUser.UserRoles.Add(userRole);
                }

                await _accountService.Registration(newUser);

                var createUserDto = new CreateUserDto
                {
                    IsCreated = true,
                    Status = "Created",
                    Code = 201
                };

                return CreatedAtAction(nameof(Registration), createUserDto);
            }
            catch (Exception ex)
            {
                var createUserDto = new CreateUserDto
                {
                    IsCreated = false,
                    Status = $"An error occurred: {ex.Message}",
                    Code = 0
                };

                return StatusCode(500, createUserDto);
            }
        }

        
    }
}
