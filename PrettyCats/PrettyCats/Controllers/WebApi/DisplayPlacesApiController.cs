using System.Collections.Generic;
using System.Web.Http;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;

namespace PrettyCats.Controllers
{
	[RoutePrefix("api/display-places")]
	public class DisplayPlacesApiController: ApiController
	{
		private readonly IKittenDisplayPlaceRepository _displayPlaceRepository;

		public DisplayPlacesApiController()
		{
			_displayPlaceRepository = new DbDisplayPlacesRepository();
		}

		[Route("")]
		public IEnumerable<DisplayPlaces> Get()
		{
			return _displayPlaceRepository.GetCollection();
		}
	}
}