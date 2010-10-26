using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DipDemo.Cataloguer.Events;
using DipDemo.Cataloguer.Infrastructure.AppController;
using DipDemo.Cataloguer.Infrastructure.Presentation;
using DipDemo.Cataloguer.Requests;
using DipDemo.Cataloguer.Services.Deserializers;

namespace DipDemo.Cataloguer.RequestHandlers
{
    public class OpenCatalogueRequestHandler : IRequestHandler<OpenCatalogueRequest>
    {
        private readonly IEnumerable<ICatalogueDeserializer> deserializers;
        private readonly IMessageBoxCreator msgCreator;
        private readonly IApplicationController appController;
        private readonly IOpenFileDialog openFileDialog;

        public OpenCatalogueRequestHandler(
            IEnumerable<ICatalogueDeserializer> deserializers,
            IMessageBoxCreator msgCreator,
            IApplicationController appController,
            IOpenFileDialog openFileDialog)
        {
            this.deserializers = deserializers;
            this.msgCreator = msgCreator;
            this.appController = appController;
            this.openFileDialog = openFileDialog;
            this.openFileDialog.Filter = BuildFilterString();
        }

        private string BuildFilterString()
        {
            if (deserializers.Count() == 0)
                throw new ApplicationException("No deserializers are present");

            var sb = new StringBuilder();
            bool isFirst = true;

            foreach (var deserializer in deserializers)
            {
                if (!isFirst)
                    sb.Append("|");

                isFirst = false;
                sb.Append(deserializer.FilterString);
            }

            return sb.ToString();
        }

        public void Execute(OpenCatalogueRequest requestData)
        {
            if (!openFileDialog.ShowDialog())
                return;

            var matchingDeserializers = deserializers.Where(x => x.IsMatch(openFileDialog.FileName)).ToList();

            if (matchingDeserializers.Count != 1)
                throw new ApplicationException("Unexpected matching deserializers count");

            var deserializer = matchingDeserializers.Single();

            try
            {
                var catalogue = deserializer.Deserialize(openFileDialog.FileName);
                catalogue.FileName = openFileDialog.FileName;
                catalogue.ResetDirty();

                appController.Raise(new CatalogueOpenedEvent(catalogue));
            }
            catch (Exception exc)
            {
                msgCreator.ShowError("Не удалось открыть каталог:" + Environment.NewLine + exc);
            }
        }
    }
}
