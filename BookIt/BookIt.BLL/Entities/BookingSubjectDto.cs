namespace BookIt.BLL.Entities
{
    public class BookingSubjectDto
    {
        public int Id { get; set; }
        public CategoryDto Category { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
        public UserDto Owner { get; set; }
        public int Capacity { get; set; }     
    }
}