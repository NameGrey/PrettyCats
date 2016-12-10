﻿using System.Collections.Generic;
using System.Data.Entity;
using PrettyCats.DAL.Entities;

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
					ShortName = "Шотландцы",
					FullName = "Котята шотландской породы",
					LinkPage = "/scotland-kittens",
					PicturePath = "/Content/Resources/Breeds/Scotland.jpg",
					Name = "Scotland"
				},
				new PetBreeds()
				{
					ID = 2,
					ShortName = "Мейн-куны",
					FullName = "Котята породы Мейн-кун",
					Name = "Mainkun",
					LinkPage = "/mainkun-kittens",
					PicturePath = "/Content/Resources/Breeds/Mainkun.jpg",
				},
				new PetBreeds()
				{
					ID = 3,
					ShortName = "Бенгалы",
					FullName = "Котята бенгальской породы",
					Name = "Bengal",
					LinkPage = "/bengal-kittens",
					PicturePath = "/Content/Resources/Breeds/bengal.jpg",
				}
			});

			context.DisplayPlaces.AddRange(new List<DisplayPlaces>()
			{
				new DisplayPlaces() {ID = 1, PlaceOfDisplaying = "Отображать на сайте"},
				new DisplayPlaces() {ID = 2, PlaceOfDisplaying = "Отображать на главной"},
				new DisplayPlaces() {ID = 3, PlaceOfDisplaying = "Не отображать"}
			});

			context.Owners.AddRange(new List<Owners>()
			{
				new Owners() {ID = 1, Name = "Юлия", Phone = "8-903-640-57-96"},
				new Owners() {ID = 2, Name = "Юлия", Phone = "8-965-712-20-49"},
				new Owners() {ID = 3, Name = "Анна", Phone = "8-952-121-47-50"}
			});
		}
	}
}