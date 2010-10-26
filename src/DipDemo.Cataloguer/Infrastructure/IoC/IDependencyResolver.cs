namespace DipDemo.Cataloguer.Infrastructure.IoC
{
    public interface IDependencyResolver
    {
        T Resolve<T>();
        T TryResolve<T>();
        void Inject<T>(object instance);
    }
}