namespace DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding
{
    public interface IEventBinder
    {
        void Bind(IPresenter presenter);
    }
}