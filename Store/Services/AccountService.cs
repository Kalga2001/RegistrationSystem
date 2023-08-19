using Microsoft.EntityFrameworkCore;
using Store.Context;
using Store.Dtos.UserDto;
using Store.Entities;
using Store.Interfaces;

namespace Store.Services
{
    public class AccountService : IAccountService
    {
        private readonly StoreDbContext _context;
        public AccountService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int id)
        {
           return await _context.User.FirstOrDefaultAsync(x => x.Id == id);      
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> Login(LoginDto loginDto)
        {
          return await _context.User.FirstOrDefaultAsync(x => x.Email == loginDto.Email && x.Password == loginDto.Password);
        }

        public async Task Registration(User newUser)
        {
            var role = await _context.Role.FirstOrDefaultAsync(x => x.Name.Contains("User"));

            User user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Password = newUser.Password,
                UserRoles = new List<UserRole>() // Initialize the UserRoles collection
            };

            if (role != null)
            {
                var userRole = new UserRole
                {
                    User = user,
                    Role = role
                };

                user.UserRoles.Add(userRole);
            }

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
