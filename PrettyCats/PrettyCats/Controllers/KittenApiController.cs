using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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

		[Route("{id:int}")]
		public Pets GetById(int id)
		{
			return _kittensRepository.GetByID(id);
		}

		[Route("kittensByPath/{pathName}")]
		public IEnumerable<Pets> GetKittens(string pathName)
		{
			pathName = "/" + pathName;

			return _kittensRepository.GetCollection().Where(i => i.PetBreeds.LinkPage == pathName).ToList();
		}

		[HttpPost]
		[Route("add")]
		public void AddKitten(Pets kitten)
		{
			if (_kittensRepository.IsKittenExists(kitten))
			{
				throw new NotImplementedException();
				//TODO: define how to handle exceptions as for server side as for client side
			}

			_kittensRepository.Insert(kitten);
			_kittensRepository.Save();
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

			_kittensRepository.Update(kitten);
			_kittensRepository.Save();
		}

		[Route("")]
		public IEnumerable<Pets> GetKittens()
		{
			return _kittensRepository.GetCollection();
		}

		[Route("parents")]
		public IEnumerable<Pets> GetParents()
		{
			return _kittensRepository.GetCollection().Where(i => i.IsParent && !i.IsInArchive);
		}
	}
}