using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Infrastructure.AppController.Environment;

namespace DipDemo.Cataloguer.Services.Deserializers
{
    public class BinaryCatalogueDeserializer : ICatalogueDeserializer
    {
        private readonly IFileSystem fileSystem;

        public string FilterString
        {
            get { return "Binary catalog (*.bctlg)|*.bctlg"; }
        }

        public BinaryCatalogueDeserializer(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public Catalogue Deserialize(string path)
        {
            var formatter = new BinaryFormatter();

            byte[] buffer = fileSystem.ReadBytes(path);

            using (var ms = new MemoryStream(buffer))
            {
                return (Catalogue)formatter.Deserialize(ms);
            }
        }

        public bool IsMatch(string filePath)
        {
            return Path.GetExtension(filePath) == ".bctlg";
        }
    }
}