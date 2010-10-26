using System;
using System.Collections.Generic;

namespace DipDemo.Cataloguer.Infrastructure.EventAggregator
{
    public class EventPublisher : IEventPublisher
    {
        private IList<object> listeners = new List<object>();
        private object syncObj = new object();
        private Action<Exception> onException;

        #region Implementation of IEventPublisher

        public void Register(object handler)
        {
            WithLock(() =>
                         {
                             if (listeners.Contains(handler))
                                 return;

                             listeners.Add(handler);

                             if (handler is IDisposableHandler)
                             {
                                 ((IDisposableHandler)handler).Disposed +=
                                     () => Unregister(handler);
                             }
                         });
        }

        public void Unregister(object handler)
        {
            WithLock(() => listeners.Remove(handler));
        }

        public void Publish<T>(T data)
        {
            WithLock(() =>
                         {
                             foreach (var listener in listeners)
                             {
                                 //ctx.Post(x => Run(x, data), listener);
                                 Run(listener, data);
                             }
                         });
        }

        public void OnHandlerError(Action<Exception> action)
        {
            this.onException = action;
        }

        #endregion

        private void WithLock(Action action)
        {
            lock (syncObj)
            {
                action();
            }
        }

        private void Run<T>(object listener, T data)
        {
            var handler = listener as IEventHandler<T>;

            if (handler == null)
                return;

            try
            {
                handler.ProcessEvent(data);
            }
            catch (Exception exc)
            {
                if (onException != null)
                    onException(exc);
            }
        }
    }
}