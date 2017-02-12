using System.Collections.Generic;
using System.Web.Http;
using PrettyCats.DAL.Entities;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;

namespace PrettyCats.Controllers.WebApi
{
	[RoutePrefix("api/owners")]
	public class OwnersApiController: ApiController
	{
		private readonly IKittenOwnerRepository _ownersRepository;

		public OwnersApiController()
		{
			_ownersRepository = new DbOwnersRepository();
		}

		[Route("")]
		public IEnumerable<Owners> Get()
		{
			return _ownersRepository.GetCollection();
		}
	}
}