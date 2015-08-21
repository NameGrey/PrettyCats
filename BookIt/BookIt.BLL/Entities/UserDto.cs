namespace BookIt.BLL.Entities
{
    public class UserDto
    {
        public int Id { get; set; }
        public RoleDto Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}