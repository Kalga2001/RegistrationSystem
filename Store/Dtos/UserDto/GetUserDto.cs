using Store.Entities;

namespace Store.Dtos.UserDto
{
    public class GetUserDto
    {
        public GetUserDto()
        {
            Users = new List<User>();
            Code = 0;
            Status = string.Empty;
        }

        public List<User> Users { get; set; }
        public int Code { get; set; }

        public string Status { get; set; }
    }
}
