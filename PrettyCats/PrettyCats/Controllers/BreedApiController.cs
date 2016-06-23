using System.Collections.Generic;
using System.Web.Http;
using PrettyCats.DAL;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;

namespace PrettyCats.Controllers
{
	[RoutePrefix("api/breeds")]
	public class BreedApiController:ApiController
	{
		private readonly IKittenBreedRepository _breedRepository;

		public BreedApiController()
		{
			StorageContext context = new StorageContext();

			_breedRepository = new DbBreedsRepository(context);
		}

		[Route("/")]
		public IEnumerable<PetBreeds> Get()
		{
			return _breedRepository.GetCollection();
		}
	}
}