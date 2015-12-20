namespace PrettyCats.Database
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class Storage : DbContext
	{
		public Storage()
#if DEBUG
			: base("DBConnectionDebug")
#else
			:base("DBConnection")
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
			modelBuilder.Entity<DisplayPlaces>()
				.HasMany(e => e.Pets)
				.WithOptional(e => e.DisplayPlaces)
				.HasForeignKey(e => e.WhereDisplay);

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
				.Map(m => m.ToTable("PetsPictures").MapLeftKey("PetID").MapRightKey("PictureID"));

			modelBuilder.Entity<Pictures>()
				.Property(e => e.Image)
				.IsUnicode(false);
		}
	}
}
