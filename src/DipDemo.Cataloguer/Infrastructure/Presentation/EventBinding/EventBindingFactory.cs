namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding
{
    public class EventBindingFactory : IEventBindingFactory
    {
        private readonly IEventBinder[] binders;

        public EventBindingFactory(IEventBinder[] binders)
        {
            this.binders = binders;
        }

        public void WireEvents(IPresenter presenter)
        {
            if (presenter.View == null)
                return;

            foreach (var binder in binders)
            {
                binder.Bind(presenter);
            }
        }
    }
}