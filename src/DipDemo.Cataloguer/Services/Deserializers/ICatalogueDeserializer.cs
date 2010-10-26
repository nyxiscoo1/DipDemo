using DipDemo.Cataloguer.Core;

namespace DipDemo.Cataloguer.Services.Deserializers
{
    public interface ICatalogueDeserializer
    {
        string FilterString { get; }

        Catalogue Deserialize(string path);

        bool IsMatch(string filePath);
    }
}