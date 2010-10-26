using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Infrastructure.AppController;

namespace DipDemo.Cataloguer.Requests
{
    public class SaveCatalogueRequest : IRequestData
    {
        public Catalogue Catalogue { get; private set; }

        public SaveCatalogueRequest(Catalogue catalogue)
        {
            Catalogue = catalogue;
        }
    }
}