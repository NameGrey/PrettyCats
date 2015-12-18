using System.IO;
using System.Web;
using PrettyCats.Models;

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
		public virtual DbSet<Pictures> Pictures { get; set; }
		public virtual DbSet<Pets> Pets { get; set; }

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

			modelBuilder.Entity<Pictures>()
				.Property(e => e.Image)
				.IsUnicode(false);

			modelBuilder.Entity<Pets>()
				.Property(e => e.UnderThePictureText)
				.IsUnicode(false);
		}

		public System.Data.Entity.DbSet<PrettyCats.Models.KittenModelView> KittenModelViews { get; set; }

		public void AddNewPet(KittenModelView newKitten, string imagePath)
		{
			if (newKitten.ImageUpload.ContentLength > 0)
			{
				var newPicture = Pictures.Add(new Pictures() {Image = imagePath});
			}

			Pets.Add(new Pets()
			{
				Name = newKitten.Name,
				RussianName = newKitten.RussianName,
				BirthDate = newKitten.BirthDate,
				UnderThePictureText = newKitten.UnderThePictureText,
				BreedID = newKitten.BreedId,
				WhereDisplay = newKitten.DisplayPlaceId,
				OwnerID = newKitten.OwnerId
			});
		}

		public string GetKittenImagePath(string kittenName, HttpServerUtilityBase server)
		{
			// extract only the fielname
			var fileName = kittenName + ".jpg";
			// store the file inside /App_Data/Pictures/Award folder
			var path = Path.Combine(server.MapPath(""), fileName);

			return path;
		}
	}
}
