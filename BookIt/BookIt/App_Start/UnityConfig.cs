using Microsoft.Practices.Unity;
using System.Web.Http;
using BookIt.BLL.Entities;
using BookIt.BLL.Services;
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
            
			container.RegisterType<IUnitOfWork, UnitOfWork>();
			container.RegisterType<IGenericRepository<Subject>, SubjectsRepository>();
			container.RegisterType<IGenericRepository<Offer>, OffersRepository>();
			container.RegisterType<IGenericRepository<Category>, CategoriesRepository>();
			container.RegisterType<IGenericRepository<User>, UsersRepository>();

			container.RegisterType<IAccountService, AccountService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}