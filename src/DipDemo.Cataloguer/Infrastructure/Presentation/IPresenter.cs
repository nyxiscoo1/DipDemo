using System;
using DipDemo.Cataloguer.Infrastructure.EventAggregator;

namespace DipDemo.Cataloguer.Infrastructure.Presentation
{
    public interface IPresenter : IDisposableHandler, IDisposable
    {
        IView View { get; }
        void Close();
    }
}