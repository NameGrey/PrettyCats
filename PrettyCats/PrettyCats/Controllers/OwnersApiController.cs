using System.Collections.Generic;
using System.Web.Http;
using PrettyCats.DAL;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;

namespace PrettyCats.Controllers
{
	[RoutePrefix("api/owners")]
	public class OwnersApiController: ApiController
	{
		private readonly IKittenOwnerRepository _ownersRepository;

		public OwnersApiController()
		{
			StorageContext context = new StorageContext();

			_ownersRepository = new DbOwnersRepository(context);
		}

		[Route("")]
		public IEnumerable<Owners> Get()
		{
			return _ownersRepository.GetCollection();
		}
	}
}