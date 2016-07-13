using Microsoft.Practices.Unity;
using System.Web.Http;
using PrettyCats.DAL.Repositories;
using PrettyCats.DAL.Repositories.DbRepositories;
using PrettyCats.Helpers;
using PrettyCats.Services;
using PrettyCats.Services.Interfaces;
using Unity.WebApi;

namespace PrettyCats
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();

			container.RegisterType<IKittensRepository, DBKittensRepository>();
			container.RegisterType<IPicturesRepository, DbPicturesRepository>();
			container.RegisterType<IKittenBreedRepository, DbBreedsRepository>();
			container.RegisterType<IKittenDisplayPlaceRepository, DbDisplayPlacesRepository>();
			container.RegisterType<IKittenOwnerRepository, DbOwnersRepository>();

			container.RegisterType<IPictureLinksConstructor, PicturesLinksConstructor>();
			container.RegisterType<PicturesLinksConstructor>(new InjectionConstructor(GlobalAppConfiguration.BaseServerUrl));

			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
		}
	}
}