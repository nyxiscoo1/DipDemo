namespace DipDemo.Cataloguer.Infrastructure.AppController
{
    public interface IApplicationController
    {
        void ProcessRequest<T>(T requestData) where T : IRequestData;
        void ProcessRequest<T>() where T : IRequestData, new();

        void Raise<T>(T eventData);
        void Raise<T>() where T : new();

        TResponse RequestReply<TRequest, TResponse>(TRequest request) where TRequest : IRequestData<TResponse>;
    }
}