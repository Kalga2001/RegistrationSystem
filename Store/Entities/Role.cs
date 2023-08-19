using System.Text.Json.Serialization;

namespace Store.Entities
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<UserRole>? UserRoles { get; set; }
    } 
}
