using System.Windows.Forms;
using DipDemo.Cataloguer.Infrastructure.AppController;
using DipDemo.Cataloguer.Infrastructure.EventAggregator;
using DipDemo.Cataloguer.Infrastructure.IoC;
using DipDemo.Cataloguer.Infrastructure.IoC.StructureMap;
using DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding;
using DipDemo.Cataloguer.Services.Deserializers;
using DipDemo.Cataloguer.Services.Serializers;
using StructureMap.Configuration.DSL;

namespace DipDemo.Cataloguer
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            #region ApplicationController and EventAggregator support

            ForSingletonOf<IEventPublisher>()
                .Use<EventPublisher>();

            RegisterInterceptor(new EventAggregatorInterceptor());

            For<IDependencyResolver>()
                .Use<StructureMapDependencyResolver>();

            For<IApplicationController>()
                .Use<ApplicationController>();

            #endregion

            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();

                x.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                x.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));

                x.AddAllTypesOf<IEventBinder>();
                x.AddAllTypesOf<ICatalogueSerializer>();
                x.AddAllTypesOf<ICatalogueDeserializer>();
            });

            RegisterInterceptor(new EventBindingInterceptor());

            ForSingletonOf<IEventBindingFactory>()
                .Use<EventBindingFactory>();
        }
    }
}
