using Microsoft.Practices.Unity;
using System.Web.Http;
using BookIt.Repository;
using BookIt.Services;
using Unity.WebApi;

namespace BookIt
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
			container.RegisterType<IBookItRepository, DBRepository>();

			container.RegisterType<IAccountService, AccountService>();
			container.RegisterType<ICategoriesService, CategoriesService>();
			container.RegisterType<ISubjectsService, SubjectsService>();
			container.RegisterType<IOffersService, OffersService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}