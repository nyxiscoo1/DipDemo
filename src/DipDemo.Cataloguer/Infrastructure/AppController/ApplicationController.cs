using System;
using DipDemo.Cataloguer.Infrastructure.EventAggregator;
using DipDemo.Cataloguer.Infrastructure.IoC;

namespace DipDemo.Cataloguer.Infrastructure.AppController
{
    public class ApplicationController : IApplicationController
    {
        private readonly IDependencyResolver resolver;
        private readonly IEventPublisher eventPublisher;

        public ApplicationController(IDependencyResolver container, IEventPublisher eventPublisher)
        {
            resolver = container;
            this.eventPublisher = eventPublisher;

            resolver.Inject<IApplicationController>(this);
        }

        public void ProcessRequest<T>(T commandData) where T : IRequestData
        {
            var requestHandler = resolver.TryResolve<IRequestHandler<T>>();
            if (requestHandler == null)
                throw new ApplicationException("Request handler for [" + typeof (T).FullName + "] not found");

            requestHandler.Execute(commandData);
        }

        public void ProcessRequest<T>() where T : IRequestData, new()
        {
            ProcessRequest(new T());
        }

        public void Raise<T>(T eventData)
        {
            eventPublisher.Publish(eventData);
        }

        public void Raise<T>() where T : new()
        {
            Raise(new T());
        }

        public TResponse RequestReply<TRequest, TResponse>(TRequest request) where TRequest : IRequestData<TResponse>
        {
            var handler = resolver.Resolve<IRequestHandler<TRequest, TResponse>>();
            return handler.ProcessRequest(request);
        }
    }
}