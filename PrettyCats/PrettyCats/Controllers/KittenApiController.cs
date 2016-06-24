using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrettyCats.DAL;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;

namespace PrettyCats.Controllers
{
	[RoutePrefix("api/kittens")]
	public class KittenApiController: ApiController
	{
		private readonly IKittensRepository _kittensRepository;

		public KittenApiController()
		{
			StorageContext context = new StorageContext();

			_kittensRepository = new DBKittensRepository(context);
		}

		[Route("")]
		public IEnumerable<Pets> Get()
		{
			return _kittensRepository.GetCollection();
		}

		[Route("kittensByPath/{pathName}")]
		public IEnumerable<Pets> GetKittens(string pathName)
		{
			pathName = "/" + pathName;

			return _kittensRepository.GetCollection().Where(i => i.PetBreeds.LinkPage == pathName).ToList(); ;
		}

		[Route("kitten-main-image/{id}")]
		public Pictures GetMainPicture(int id)
		{
			var kitten = _kittensRepository.GetByID(id);

			return kitten.Pictures.FirstOrDefault(i => i.IsMainPicture && i.PetID == kitten.ID);
		}
	}
}