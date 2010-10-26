using DipDemo.Cataloguer.Core;

namespace DipDemo.Cataloguer.Events
{
    public class BookUpdatedEvent
    {
        public Book Book { get; private set; }

        public BookUpdatedEvent(Book book)
        {
            Book = book;
        }
    }
}