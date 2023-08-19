using Store.Dtos.UserDto;
using Store.Entities;

namespace Store.Interfaces
{
    public interface IAccountService
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> Login(LoginDto loginDto);
        Task Registration(User newUser);

    }
}
