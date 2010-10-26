using System;
using System.Text;
using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Infrastructure.AppController;
using DipDemo.Cataloguer.Infrastructure.Presentation;
using DipDemo.Cataloguer.Requests;

namespace DipDemo.Cataloguer.RequestHandlers
{
    public class CloseCatalogueRequestHandler : IRequestHandler<CloseCatalogueRequest, CloseCatalogueResponse>
    {
        public static readonly string AskForSaveMessage =
            "Каталог содержит несохраненные сведения, сохранить перед закрытием?";

        private readonly IMessageBoxCreator msgCreator;
        private readonly IApplicationController appController;

        public CloseCatalogueRequestHandler(IMessageBoxCreator msgCreator, IApplicationController appController)
        {
            this.msgCreator = msgCreator;
            this.appController = appController;
        }

        public CloseCatalogueResponse ProcessRequest(CloseCatalogueRequest request)
        {
            bool isCancelled = false;

            if (request.Catalogue != null && request.Catalogue.IsDirty)
            {
                bool? result = msgCreator.AskUserWithCancellation(AskForSaveMessage);

                if (result == true)
                {
                    if (!IsCatalogueValid(request.Catalogue))
                        isCancelled = true;
                    else
                        appController.ProcessRequest(new SaveCatalogueRequest(request.Catalogue));

                }
                else
                    isCancelled = result == null;
            }

            return new CloseCatalogueResponse(isCancelled);
        }

        private bool IsCatalogueValid(Catalogue catalogue)
        {
            var result = catalogue.Validate();

            if (!result.IsValid)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Каталог содержит ошибки и не может быть сохранен:");
                foreach (var message in result.Messages)
                {
                    sb.AppendLine("    * " + message);
                }

                sb.Append("Пожалуйста, сиправьте ошибки и повторите попытку сохранения");

                msgCreator.ShowError(sb.ToString());
            }

            return result.IsValid;
        }
    }
}
