using Autofac;

namespace FlakyApi.Implementations
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultService>().As<IService>().SingleInstance();
            builder.RegisterType<ExponentialFailureEventsStrategy>().As<IFlakyStrategy>().SingleInstance();
            builder.RegisterDecorator<DefaultFlakyService, IService>();
        }
    }
}