using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using PrettyCats.DAL;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;
using PrettyCats.Models;

namespace PrettyCats.Controllers
{
	[RoutePrefix("api/admin/kittens")]
	public class AdminApiController : ApiController
	{
		private readonly IKittensRepository _kittensRepository;
		private readonly IPicturesRepository _picturesRepository;
		private readonly IKittenBreedRepository _breedsRepository;
		private readonly IKittenOwnerRepository _ownersRepository;
		private readonly IKittenDisplayPlaceRepository _displayPlacesRepository;

		public AdminApiController()
		{
			var newContext = new StorageContext();

			_kittensRepository = new DBKittensRepository(newContext);
			_picturesRepository = new DbPicturesRepository(newContext);
			_breedsRepository = new DbBreedsRepository(newContext);
			_ownersRepository = new DbOwnersRepository(newContext);
			_displayPlacesRepository = new DbDisplayPlacesRepository(newContext);

			CustomizeAutomapper();
		}

		private void CustomizeAutomapper()
		{
			AutoMapper.Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Pets, KittenOnTheAdminPageModelView>()
					.ForMember(i => i.PlaceOfDisplaying, i => i.MapFrom(m => m.DisplayPlace.PlaceOfDisplaying))
					.ForMember(i => i.ImageUrl,
						i => i.MapFrom(
							src => src.Pictures.FirstOrDefault(el => el.IsMainPicture) != null ? src.Pictures.First(el => el.IsMainPicture).Image : string.Empty))
					.ForMember(i => i.AllParents, i => i.UseValue(_kittensRepository.GetCollection().Where(el => el.IsParent)))
					.ForMember(i => i.DisplayPlaces, i => i.UseValue(_displayPlacesRepository.GetCollection()))
					.ForMember(i => i.Breeds, i => i.UseValue(_breedsRepository.GetCollection()))
					.ForMember(i => i.Owners, i => i.UseValue(_ownersRepository.GetCollection()))
					.ForMember(i => i.PictureID,
						i => i.MapFrom(
							src => src.Pictures.FirstOrDefault(el => el.IsMainPicture) != null ? (int?)src.Pictures.First(el => el.IsMainPicture).ID : null));

				cfg.CreateMap<Pets, AddKittenModelView>()
					.ForMember(i => i.BirthDate, i => i.MapFrom(src => src.BirthDate != null ? ((DateTime)src.BirthDate).ToString("dd.MM.yyyy") : string.Empty))
					.ForMember(i => i.BreedName, i => i.MapFrom(src => src.PetBreeds != null ? src.PetBreeds.ShortName : string.Empty))
					.ForMember(i => i.FatherName, i => i.MapFrom(src => src.Father != null ? src.Father.RussianName : string.Empty))
					.ForMember(i => i.MotherName, i => i.MapFrom(src => src.Mother != null ? src.Mother.RussianName : string.Empty))
					.ForMember(i => i.OwnerName, i => i.MapFrom(src => src.Owners != null ? src.Owners.Name : string.Empty))
					.ForMember(i => i.OwnerPhone, i => i.MapFrom(src => src.Owners != null ? src.Owners.Phone : string.Empty))
					.ForMember(i => i.Owners, i => i.UseValue(_ownersRepository.GetCollection()))
					.ForMember(i => i.AllParents, i => i.UseValue(_kittensRepository.GetCollection().Where(el => el.IsParent)))
					.ForMember(i => i.DisplayPlaces, i => i.UseValue(_displayPlacesRepository.GetCollection()))
					.ForMember(i => i.Breeds, i => i.UseValue(_breedsRepository.GetCollection()));
			});

		}

		[Route("")]
		public IEnumerable<KittenOnTheAdminPageModelView> GetKittens()
		{
			var kittens = _kittensRepository.GetCollection().Where(e => e.IsParent == false && e.IsInArchive == false);
			var kittensModelViews = AutoMapper.Mapper.Map<IEnumerable<Pets>, IEnumerable<KittenOnTheAdminPageModelView>>(kittens);

			//var str = JsonConvert.SerializeObject(kittensModelViews, Formatting.None,
			//	new JsonSerializerSettings() {PreserveReferencesHandling = PreserveReferencesHandling.All});


			return kittensModelViews;
		}

		[HttpPost]
		[Route("edit")]
		public void EditKitten(Pets kitten)
		{
			if (_kittensRepository.IsKittenExistsWithAnotherId(kitten))
			{
				throw new NotImplementedException();
				//TODO: define how to handle exceptions as for server side as for client side
			}

			// Initialize addition fields
			kitten.Owners = _ownersRepository.GetByID(kitten.OwnerID);
			kitten.PetBreeds = _breedsRepository.GetByID(kitten.BreedID);

			_kittensRepository.Update(kitten);
			_kittensRepository.Save();
		}
	}
}
