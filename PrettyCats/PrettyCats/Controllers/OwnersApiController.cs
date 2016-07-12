using System.Collections.Generic;
using System.Web.Http;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;

namespace PrettyCats.Controllers
{
	[RoutePrefix("api/owners")]
	public class OwnersApiController: ApiController
	{
		private readonly IKittenOwnerRepository _ownersRepository;

		public OwnersApiController(IKittenOwnerRepository ownersRepository)
		{
			_ownersRepository = ownersRepository;
		}

		[Route("")]
		public IEnumerable<Owners> Get()
		{
			return _ownersRepository.GetCollection();
		}
	}
}