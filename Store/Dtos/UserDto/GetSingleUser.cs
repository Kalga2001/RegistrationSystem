using Store.Entities;

namespace Store.Dtos.UserDto
{
    public class GetSingleUser
    {
        public GetSingleUser()
        {
            User = new User();
            Code = 0;
            Status = string.Empty;
        }

        public User User { get; set; }
        public int Code { get; set; }

        public string Status { get; set; }
    }
}
