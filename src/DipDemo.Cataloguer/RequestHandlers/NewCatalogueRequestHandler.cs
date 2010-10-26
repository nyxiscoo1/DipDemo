using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Events;
using DipDemo.Cataloguer.Infrastructure.AppController;
using DipDemo.Cataloguer.Requests;

namespace DipDemo.Cataloguer.RequestHandlers
{
    public class NewCatalogueRequestHandler : IRequestHandler<NewCatalogueRequest>
    {
        private readonly IApplicationController appController;

        public NewCatalogueRequestHandler(IApplicationController appController)
        {
            this.appController = appController;
        }

        public void Execute(NewCatalogueRequest requestData)
        {
            var response = appController.RequestReply<CloseCatalogueRequest, CloseCatalogueResponse>(
                new CloseCatalogueRequest(requestData.CurrentCatalogue));

            if (response.IsCancelled)
                return;

            appController.Raise(new CatalogueOpenedEvent(new Catalogue()));
        }
    }
}
