using System;

namespace DipDemo.Cataloguer.Infrastructure.EventAggregator
{
    public interface IEventPublisher
    {
        void Register(object handler);
        void Unregister(object handler);
        void Publish<T>(T data);
        void OnHandlerError(Action<Exception> action);
    }
}