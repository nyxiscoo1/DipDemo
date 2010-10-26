using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Infrastructure.AppController;

namespace DipDemo.Cataloguer.Requests
{
    public class SaveCatalogueAsRequest : IRequestData
    {
        public Catalogue Catalogue { get; private set; }

        public SaveCatalogueAsRequest(Catalogue catalogue)
        {
            Catalogue = catalogue;
        }
    }
}