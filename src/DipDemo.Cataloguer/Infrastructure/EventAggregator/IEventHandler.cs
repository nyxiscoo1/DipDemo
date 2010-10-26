namespace DipDemo.Cataloguer.Infrastructure.EventAggregator
{
    public interface IEventHandler<T>
    {
        void ProcessEvent(T data);
    }
}