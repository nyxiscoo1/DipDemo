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
    public class SaveCatalogueAsRequestHandler : IRequestHandler<SaveCatalogueAsRequest>
    {
        private readonly IEnumerable<ICatalogueSerializer> serializers;
        private readonly IApplicationController appController;
        private readonly IMessageBoxCreator msgCreator;
        private readonly ISaveFileDialog saveFileDialog;

        public SaveCatalogueAsRequestHandler(
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

        public void Execute(SaveCatalogueAsRequest requestData)
        {
            if (!saveFileDialog.ShowDialog())
                return;

            var matchingDeserializers = serializers.Where(x => x.IsMatch(saveFileDialog.FileName)).ToList();

            if (matchingDeserializers.Count != 1)
                throw new ApplicationException("Unexpected matching serializers count");

            var serializer = matchingDeserializers.Single();

            try
            {
                serializer.Serialize(saveFileDialog.FileName, requestData.Catalogue);

                requestData.Catalogue.FileName = saveFileDialog.FileName;
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