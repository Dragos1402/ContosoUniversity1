using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieTest2017.App_Start
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<GeneralServ>().As<IGeneralServ>();
            //builder.RegisterApiControllers(typeof(GeneralController).Assembly);
           
            return builder.Build();
        }
    }
}
