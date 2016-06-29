using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrettyCats.DAL;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;

namespace PrettyCats.Controllers
{
	[RoutePrefix("api/pictures")]
	public class PicturesApiController: ApiController
	{
		private readonly IPicturesRepository _picturesRepository;

		public PicturesApiController()
		{
			StorageContext context = new StorageContext();

			_picturesRepository = new DbPicturesRepository(context);
		}

		[Route("{kittenId}")]
		public IEnumerable<Pictures> GetCollectionByKittenId(int kittenId)
		{
			return _picturesRepository.GetCollection().Where(i => i.PetID == kittenId).OrderBy(i => i.Order);
		}

		[Route("main-picture/{kittenId}")]
		public Pictures GetMainPicture(int kittenId)
		{
			return _picturesRepository.GetCollection().FirstOrDefault(i=>i.IsMainPicture && i.PetID == kittenId);
		}
	}
}