using System;

namespace DipDemo.Cataloguer.Infrastructure.EventAggregator
{
    public interface IDisposableHandler
    {
        event Action Disposed;
    }
}