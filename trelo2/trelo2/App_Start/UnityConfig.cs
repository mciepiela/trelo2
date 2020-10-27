using System.Web.Mvc;
using trelo2.Services;
using trelo2.Services.Interfaces;
using Unity;
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
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}