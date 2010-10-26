using System.Windows.Forms;
using StructureMap;

namespace DipDemo.Cataloguer
{
    public static class Bootstrapper
    {
        private static IContainer Container { get; set; }

        public static ApplicationContext Configure()
        {
            Container = new Container();
            Container.Configure(c => c.AddRegistry<DefaultRegistry>());
            return Container.GetInstance<ApplicationContext>();
        }
    }
}
