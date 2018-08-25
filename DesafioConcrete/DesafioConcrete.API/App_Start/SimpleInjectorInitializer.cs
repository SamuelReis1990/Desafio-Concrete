[assembly: WebActivator.PostApplicationStartMethod(typeof(DesafioConcrete.API.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace DesafioConcrete.API.App_Start
{
    using DesafioConcrete.Dominio.Interfaces;
    using DesafioConcrete.Infra.Repositorios;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using SimpleInjector.Integration.WebApi;
    using System.Web.Http;
    using System.Web.Mvc;

    public static class SimpleInjectorInitializer
    {        
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);
            
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IRepositorioUsuario, RepositorioUsuario>(Lifestyle.Singleton);
            container.Register<IRepositorioTelefone, RepositorioTelefone>(Lifestyle.Singleton);

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }
    }
}