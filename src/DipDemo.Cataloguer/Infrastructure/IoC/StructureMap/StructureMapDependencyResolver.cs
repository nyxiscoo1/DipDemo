using StructureMap;

namespace DipDemo.Cataloguer.Infrastructure.IoC.StructureMap
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer container;

        public StructureMapDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        #region IDependencyResolver Members

        public T Resolve<T>()
        {
            return container.GetInstance<T>();
        }

        public void Inject<T>(object instance)
        {
            container.Inject(typeof(T), instance);
        }

        public T TryResolve<T>()
        {
            return container.TryGetInstance<T>();
        }

        #endregion
    }
}