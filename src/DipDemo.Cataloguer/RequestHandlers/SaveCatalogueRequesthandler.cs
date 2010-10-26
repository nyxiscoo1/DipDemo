using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DipDemo.Cataloguer.Events;
using DipDemo.Cataloguer.Infrastructure.AppController;
using DipDemo.Cataloguer.Infrastructure.Presentation;
using DipDemo.Cataloguer.Requests;
using DipDemo.Cataloguer.Services.Serializers;

namespace DipDemo.Cataloguer.RequestHandlers
{
    public class SaveCatalogueRequestHandler : IRequestHandler<SaveCatalogueRequest>
    {
        private readonly IEnumerable<ICatalogueSerializer> serializers;
        private readonly IApplicationController appController;
        private readonly IMessageBoxCreator msgCreator;
        private readonly ISaveFileDialog saveFileDialog;

        public SaveCatalogueRequestHandler(
            IEnumerable<ICatalogueSerializer> serializers,
            IApplicationController appController,
            IMessageBoxCreator msgCreator,
            ISaveFileDialog saveFileDialog)
        {
            this.serializers = serializers;
            this.appController = appController;
            this.msgCreator = msgCreator;
            this.saveFileDialog = saveFileDialog;
            this.saveFileDialog.Filter = BuildFilterString();
        }

        private string BuildFilterString()
        {
            if (serializers.Count() == 0)
                throw new ApplicationException("No serializers are present");

            var sb = new StringBuilder();
            bool isFirst = true;

            foreach (var deserializer in serializers)
            {
                if (!isFirst)
                    sb.Append("|");

                isFirst = false;
                sb.Append(deserializer.FilterString);
            }

            return sb.ToString();
        }

        public void Execute(SaveCatalogueRequest requestData)
        {
            string filePath = requestData.Catalogue.FileName;

            if (string.IsNullOrEmpty(filePath))
                if (!saveFileDialog.ShowDialog())
                    return;
                else
                    filePath = saveFileDialog.FileName;

            var matchingDeserializers = serializers.Where(x => x.IsMatch(filePath)).ToList();

            if (matchingDeserializers.Count != 1)
                throw new ApplicationException("Unexpected matching serializers count");

            var serializer = matchingDeserializers.Single();

            try
            {
                serializer.Serialize(filePath, requestData.Catalogue);

                requestData.Catalogue.FileName = filePath;
                requestData.Catalogue.ResetDirty();

                appController.Raise(new CatalogueSavedEvent(requestData.Catalogue));
            }
            catch (Exception exc)
            {
                msgCreator.ShowError("Не удалось сохранить каталог:" + Environment.NewLine + exc);
            }
        }
    }
}