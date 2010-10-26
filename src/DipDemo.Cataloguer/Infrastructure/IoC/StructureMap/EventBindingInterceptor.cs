using System;
using System.Linq;
using DipDemo.Cataloguer.Infrastructure.Presentation;
using DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding;
using StructureMap;
using StructureMap.Interceptors;

namespace DipDemo.Cataloguer.Infrastructure.IoC.StructureMap
{
    public class EventBindingInterceptor : TypeInterceptor
    {
        public object Process(object target, IContext context)
        {
            var factory = context.GetInstance<IEventBindingFactory>();
            factory.WireEvents(target as IPresenter);
            return target;
        }

        public bool MatchesType(Type type)
        {
            bool matchesType = type.GetInterfaces().Contains(typeof(IPresenter));
            return matchesType;
        }

    }
}