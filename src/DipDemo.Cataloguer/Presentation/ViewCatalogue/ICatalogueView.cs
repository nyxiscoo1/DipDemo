using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Infrastructure.Presentation;

namespace DipDemo.Cataloguer.Presentation.ViewCatalogue
{
    public interface ICatalogueView : IView
    {
        void SetModel(Catalogue catalogue);
        void UpdateModel();
    }
}