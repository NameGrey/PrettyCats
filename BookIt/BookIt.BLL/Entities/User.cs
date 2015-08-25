namespace BookIt.BLL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}