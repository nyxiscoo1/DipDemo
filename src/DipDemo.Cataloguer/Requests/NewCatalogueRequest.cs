using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Infrastructure.AppController;

namespace DipDemo.Cataloguer.Requests
{
    public class NewCatalogueRequest : IRequestData
    {
        public Catalogue CurrentCatalogue { get; private set; }

        public NewCatalogueRequest(Catalogue currentCatalogue)
        {
            CurrentCatalogue = currentCatalogue;
        }
    }
}