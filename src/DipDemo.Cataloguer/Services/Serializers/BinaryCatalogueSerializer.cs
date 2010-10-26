using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Infrastructure.AppController.Environment;

namespace DipDemo.Cataloguer.Services.Serializers
{
    public class BinaryCatalogueSerializer : ICatalogueSerializer
    {
        private readonly IFileSystem fileSystem;

        public string FilterString
        {
            get { return "Binary catalog (*.bctlg)|*.bctlg"; }
        }

        public BinaryCatalogueSerializer(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void Serialize(string path, Catalogue catalogue)
        {
            var formatter = new BinaryFormatter();

            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, catalogue);
                fileSystem.WriteBytes(path, ms.ToArray());
            }

            
        }

        public bool IsMatch(string filePath)
        {
            return Path.GetExtension(filePath) == ".bctlg";
        }
    }
}