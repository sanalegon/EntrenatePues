using Autofac;

namespace EntrenatePues.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            _ = builder.RegisterAssemblyTypes(typeof(CoreModule).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
