namespace Store.Dtos.UserDto
{
    public class CreateUserDto
    {
        public CreateUserDto()
        {
            IsCreated = false;
            Code = 0;
            Status = string.Empty;
        }
        public int Code { get; set; }
        public string Status { get; set; }
        public bool IsCreated { get; set; }
    }
}
