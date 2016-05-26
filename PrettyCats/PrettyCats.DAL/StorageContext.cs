using System.Data.Entity;
using PrettyCats.DAL.Entities;

namespace PrettyCats.DAL
{
	public class StorageContext : DbContext
	{
		public StorageContext()
#if DEBUG
			: base("DBConnectionDebugLocal")
#else
			: base("DBConnectionDebug")
#endif
		{
			Database.SetInitializer(new DatabaseInitializer());
		}

		public virtual DbSet<DisplayPlaces> DisplayPlaces { get; set; }
		public virtual DbSet<Owners> Owners { get; set; }
		public virtual DbSet<Pages> Pages { get; set; }
		public virtual DbSet<PetBreeds> PetBreeds { get; set; }
		public virtual DbSet<Pets> Pets { get; set; }
		public virtual DbSet<Pictures> Pictures { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Pets>()
				.HasRequired(i=>i.Owners)
				.WithMany()
				.HasForeignKey(i=>i.OwnerID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Pets>()
				.HasOptional(i => i.Mother)
				.WithMany()
				.HasForeignKey(i => i.MotherID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Pets>()
				.HasOptional(i => i.Father)
				.WithMany()
				.HasForeignKey(i => i.FatherID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Pets>()
				.HasRequired(i=>i.PetBreeds)
				.WithMany()
				.HasForeignKey(i=>i.BreedID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Pets>()
				.HasRequired(i => i.DisplayPlace)
				.WithMany()
				.HasForeignKey(i => i.WhereDisplay);
		}
	}
}
