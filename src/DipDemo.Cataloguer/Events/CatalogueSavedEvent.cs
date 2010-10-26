using DipDemo.Cataloguer.Core;

namespace DipDemo.Cataloguer.Events
{
    public class CatalogueSavedEvent
    {
        public Catalogue Catalogue { get; private set; }

        public CatalogueSavedEvent(Catalogue catalogue)
        {
            Catalogue = catalogue;
        }
    }
}