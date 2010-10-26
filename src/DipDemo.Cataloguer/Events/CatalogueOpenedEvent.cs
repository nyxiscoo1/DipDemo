using DipDemo.Cataloguer.Core;

namespace DipDemo.Cataloguer.Events
{
    public class CatalogueOpenedEvent
    {
        public Catalogue Catalogue { get; private set; }

        public CatalogueOpenedEvent(Catalogue catalogue)
        {
            Catalogue = catalogue;
        }
    }
}