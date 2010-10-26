using DipDemo.Cataloguer.Infrastructure.Presentation;

namespace DipDemo.Cataloguer.Presentation.EditBook
{
    public interface IEditBookView : IView
    {
        string Author { get; set; }
        string Description { get; set; }
        string Name { get; set; }
    }
}