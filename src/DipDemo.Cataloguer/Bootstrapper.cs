using System.Windows.Forms;
using DipDemo.Cataloguer.Presentation;
using DipDemo.Cataloguer.Presentation.ViewCatalogue;
using StructureMap;

namespace DipDemo.Cataloguer
{
    public static class Bootstrapper
    {
        private static IContainer Container { get; set; }

        public static Form Configure()
        {
            Container = new Container();
            Container.Configure(c => c.AddRegistry<DefaultRegistry>());

            var mainForm = Container.GetInstance<AppShell>();
            Container.GetInstance<CataloguePresenter>();

            return mainForm;
        }
    }
}
