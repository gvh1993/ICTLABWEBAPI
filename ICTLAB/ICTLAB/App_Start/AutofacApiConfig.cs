using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ICTLAB.App_Start;
using ICTLAB.Services;

namespace ICTLAB
{
    public class AutofacApiConfig
    {
        public static void RegisterAutofac(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            // You can register controllers all at once using assembly scanning...
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //register the services and repositories
            builder.RegisterModule(new AutofacModule());

            //build the container
            var container = builder.Build();

            //start the dependency resolver
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            
        }
    }
}