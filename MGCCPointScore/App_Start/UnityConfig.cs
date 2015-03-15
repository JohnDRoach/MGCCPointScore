using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Repositories.Members;
using MGCCPointScore.Models;
using Microsoft.AspNet.Identity;
using Repositories.Identity;
using MongoDB.AspNet.Identity;

namespace MGCCPointScore
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterInstance<IMemberRepository>(MemberRepositoryFactory.Create());
            container.RegisterType<UserManager<ApplicationUser>>(new TransientLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new TransientLifetimeManager(), new InjectionFactory(c => MyUserStoreFactory.Create<ApplicationUser>()));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
