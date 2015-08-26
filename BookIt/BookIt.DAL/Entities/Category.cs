namespace BookIt.DAL.Entities
{
    public class Category : IEntity
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}