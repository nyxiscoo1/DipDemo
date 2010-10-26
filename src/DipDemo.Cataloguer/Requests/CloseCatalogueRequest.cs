using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Infrastructure.AppController;

namespace DipDemo.Cataloguer.Requests
{
    public class CloseCatalogueRequest : IRequestData<CloseCatalogueResponse>
    {
        public Catalogue Catalogue { get; private set; }

        public CloseCatalogueRequest(Catalogue catalogue)
        {
            Catalogue = catalogue;
        }
    }

    public class CloseCatalogueResponse
    {
        public bool IsCancelled { get; private set; }

        public CloseCatalogueResponse(bool isCancelled)
        {
            IsCancelled = isCancelled;
        }
    }
}