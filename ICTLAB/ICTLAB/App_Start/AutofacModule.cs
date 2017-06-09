using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using ICTLAB.Repositories;
using ICTLAB.Services;

namespace ICTLAB.App_Start
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //register all services
            builder.RegisterType<SensorService>().As<ISensorService>();
            builder.RegisterType<HomeService>().As<IHomeService>();

            //register all repositories
            builder.RegisterType<SensorRepository>().As<ISensorRepository>();
            builder.RegisterType<HomeRepository>().As<IHomeRepository>();

            //TODO register log4net
            
        }
    }
}