using System.Data.Entity;

namespace PrettyCats.DAL
{
	public partial class Storage : DbContext
	{
		public Storage()
#if DEBUG
			: base("DBConnectionDebugLocal")
#else
			: base("DBConnectionDebug")
#endif
		{
		}

		public virtual DbSet<DisplayPlaces> DisplayPlaces { get; set; }
		public virtual DbSet<Owners> Owners { get; set; }
		public virtual DbSet<Pages> Pages { get; set; }
		public virtual DbSet<PetBreeds> PetBreeds { get; set; }
		public virtual DbSet<Pets> Pets { get; set; }
		public virtual DbSet<Pictures> Pictures { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Owners>()
				.HasMany(e => e.Pets)
				.WithRequired(e => e.Owners)
				.HasForeignKey(e => e.OwnerID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Pages>()
				.Property(e => e.Content)
				.IsUnicode(false);

			modelBuilder.Entity<PetBreeds>()
				.Property(e => e.Description)
				.IsUnicode(false);

			modelBuilder.Entity<PetBreeds>()
				.HasMany(e => e.Pets)
				.WithRequired(e => e.PetBreeds)
				.HasForeignKey(e => e.BreedID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Pets>()
				.Property(e => e.UnderThePictureText)
				.IsUnicode(false);

			modelBuilder.Entity<Pets>()
				.HasMany(e => e.Pictures)
				.WithMany(e => e.Pets)
				.Map(m => m.ToTable("PetPictures").MapLeftKey("KittenID").MapRightKey("PictureID"));

			modelBuilder.Entity<Pictures>()
				.Property(e => e.Image)
				.IsUnicode(false);

			modelBuilder.Entity<Pictures>()
				.Property(e => e.ImageSmall)
				.IsUnicode(false);
		}
	}
}
