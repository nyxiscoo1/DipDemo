using System;

namespace DipDemo.Cataloguer.Infrastructure.Presentation
{
    public class AbstractPresenter<TView> : IPresenter
        where TView : IView
    {
        protected TView View;

        public event Action Disposed = delegate { };

        IView IPresenter.View
        {
            get { return View; }
        }

        public AbstractPresenter(TView view)
        {
            View = view;
            View.Closed += (o, e) => Dispose();
        }

        public virtual void Close()
        {
            View.CloseView();
        }

        public virtual void Dispose()
        {
            Disposed();
        }
    }
}
