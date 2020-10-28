using System.Web.Mvc;
using trelo2.Controllers;
using trelo2.Services;
using trelo2.Services.Interfaces;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace trelo2
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ITasksServices, TasksServices>();
            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}