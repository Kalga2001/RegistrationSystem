using System.Text.Json.Serialization;

namespace Store.Entities
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    

        public string Email { get; set; }   
        public string Password { get; set; }
        [JsonIgnore]
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
