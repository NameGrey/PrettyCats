using System.Collections.Generic;
using System.Web.Http;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;

namespace PrettyCats.Controllers.WebApi
{
	[RoutePrefix("api/breeds")]
	public class BreedApiController:ApiController
	{
		private readonly IKittenBreedRepository _breedRepository;

		public BreedApiController()
		{			
			_breedRepository = new DbBreedsRepository();
		}

		[Route("")]
		[HttpGet]
		public IEnumerable<PetBreeds> Get()
		{
			return _breedRepository.GetCollection();
		}
	}
}