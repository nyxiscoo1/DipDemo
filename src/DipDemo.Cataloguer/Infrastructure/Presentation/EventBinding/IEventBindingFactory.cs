namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding
{
    public interface IEventBindingFactory
    {
        void WireEvents(IPresenter presenter);
    }
}