using System.Collections.Generic;
using System.Web.Http;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;

namespace PrettyCats.Controllers.WebApi
{
	[RoutePrefix("api/breeds")]
	public class BreedApiController:ApiController
	{
		private readonly IKittenBreedRepository _breedRepository;

		public BreedApiController(IKittenBreedRepository breedRepository)
		{
			_breedRepository = breedRepository;
		}

		[Route("")]
		[HttpGet]
		public IEnumerable<PetBreeds> Get()
		{
			return _breedRepository.GetCollection();
		}
	}
}