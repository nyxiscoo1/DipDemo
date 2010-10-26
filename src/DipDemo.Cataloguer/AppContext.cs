using System.Windows.Forms;
using DipDemo.Cataloguer.Infrastructure.IoC;
using DipDemo.Cataloguer.Presentation;
using DipDemo.Cataloguer.Presentation.ViewCatalogue;

namespace DipDemo.Cataloguer
{
    public class AppContext : ApplicationContext
    {
        private readonly IDependencyResolver container;

        public AppContext(IDependencyResolver container)
        {
            this.container = container;
            MainForm = GetMainForm();
        }

        private Form GetMainForm()
        {
            var mainForm = container.Resolve<AppShell>();
            container.Resolve<CataloguePresenter>();
            return mainForm;
        }
    }
}
