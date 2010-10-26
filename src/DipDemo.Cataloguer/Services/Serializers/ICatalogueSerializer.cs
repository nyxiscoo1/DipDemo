using DipDemo.Cataloguer.Core;

namespace DipDemo.Cataloguer.Services.Serializers
{
    public interface ICatalogueSerializer
    {
        string FilterString { get; }

        bool IsMatch(string fileName);
        void Serialize(string fileName, Catalogue catalogue);
    }
}