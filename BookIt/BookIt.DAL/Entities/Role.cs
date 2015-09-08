using System.ComponentModel.DataAnnotations;

namespace BookIt.DAL.Entities
{
    public class Role : IEntity
	{
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
	}
}

