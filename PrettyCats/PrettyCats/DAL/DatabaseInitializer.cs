using System.Collections.Generic;
using System.Data.Entity;
using PrettyCats.DAL.Enteties;
using PrettyCats.DAL.Repositories;

namespace PrettyCats.DAL
{
	public class DatabaseInitializer : CreateDatabaseIfNotExists<StorageContext>
	{
		protected override void Seed(StorageContext context)
		{
			FillTestData(context);
			base.Seed(context);
		}

		private void FillTestData(StorageContext context)
		{
			context.PetBreeds.AddRange(new List<PetBreeds>()
			{
				new PetBreeds()
				{
					ID = 1,
					Description = "",
					RussianName = "Шотландцы",
					Name = "Scotland"
				},
				new PetBreeds()
				{
					ID = 2,
					Description = "",
					RussianName = "Мейн-куны",
					Name = "Mainkun"
				},
				new PetBreeds()
				{
					ID = 3,
					Description = "",
					RussianName = "Бенгалы",
					Name = "Bengal"
				}
			});

			context.DisplayPlaces.AddRange(new List<DisplayPlaces>()
			{
				new DisplayPlaces() {ID = 1, PlaceOfDisplaying = "Отображать на сайте"},
				new DisplayPlaces() {ID = 2, PlaceOfDisplaying = "Отображать на главной"},
				new DisplayPlaces() {ID = 3, PlaceOfDisplaying = "Не отображать"}
			});
		}
	}
}