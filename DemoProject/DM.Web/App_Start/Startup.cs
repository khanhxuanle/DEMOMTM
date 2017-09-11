using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using DM.Data;
using DM.Service.Services;
using DM.Service.IServices;

[assembly: OwinStartup(typeof(DM.Web.Startup))]

namespace DM.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigAutoFac(app);
        }

        private void ConfigAutoFac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<DMService>().As<IDMService>().InstancePerRequest();
            builder.RegisterType<DMContext>().AsSelf().InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
