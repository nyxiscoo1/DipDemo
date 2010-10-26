namespace DipDemo.Cataloguer.Infrastructure.AppController
{
    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequestData<TResponse>
    {
        TResponse ProcessRequest(TRequest request);
    }

    public interface IRequestHandler<T> where T : IRequestData
    {
        void Execute(T requestData);
    }
}