using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Infrastructure.AppController;

namespace DipDemo.Cataloguer.Requests
{
    public class EditBookRequest : IRequestData
    {
        public Book Book { get; private set; }

        public EditBookRequest(Book book)
        {
            Book = book;
        }
    }
}