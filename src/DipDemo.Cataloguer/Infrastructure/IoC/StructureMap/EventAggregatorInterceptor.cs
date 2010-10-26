using System;
using DipDemo.Cataloguer.Infrastructure.EventAggregator;
using StructureMap;
using StructureMap.Interceptors;
using StructureMap.TypeRules;

namespace DipDemo.Cataloguer.Infrastructure.IoC.StructureMap
{
    public class EventAggregatorInterceptor : TypeInterceptor
    {
        public object Process(object target, IContext context)
        {
            IEventPublisher eventPublisher = context.GetInstance<IEventPublisher>();
            eventPublisher.Register(target);
            return target;
        }

        public bool MatchesType(Type type)
        {
            bool matchesType = type.ImplementsInterfaceTemplate(typeof(IEventHandler<>));
            return matchesType;
        }
    }
}