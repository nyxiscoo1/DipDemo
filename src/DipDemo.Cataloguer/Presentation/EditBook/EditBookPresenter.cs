using System.Text;
using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Events;
using DipDemo.Cataloguer.Infrastructure.AppController;
using DipDemo.Cataloguer.Infrastructure.Presentation;
using DipDemo.Cataloguer.Requests;

namespace DipDemo.Cataloguer.Presentation.EditBook
{
    public class EditBookPresenter : AbstractPresenter<IEditBookView>, IRequestHandler<EditBookRequest>
    {
        private readonly IMessageBoxCreator msgCreator;
        private readonly IApplicationController appController;
        private Book book;

        public EditBookPresenter(IEditBookView view, IMessageBoxCreator msgCreator, IApplicationController appController)
            : base(view)
        {
            this.msgCreator = msgCreator;
            this.appController = appController;
        }

        public void Execute(EditBookRequest requestData)
        {
            book = requestData.Book;

            View.Name = book.Name;
            View.Author = book.Author;
            View.Description = book.Description;

            View.ShowModalView();
        }

        public void OnSave()
        {
            if (!IsEnteredDataValid())
                return;

            book.Name = View.Name;
            book.Author = View.Author;
            book.Description = View.Description;

            appController.Raise(new BookUpdatedEvent(book));

            View.CloseView();
        }

        private bool IsEnteredDataValid()
        {
            bool isValid = true;

            var sb = new StringBuilder();
            sb.AppendLine("Ошибка:");
            if (string.IsNullOrEmpty(View.Name))
            {
                sb.AppendLine("   * Не введено название");
                isValid = false;
            }

            if (string.IsNullOrEmpty(View.Author))
            {
                sb.AppendLine("   * Не введен автор");
                isValid = false;
            }

            if (!isValid)
                msgCreator.ShowError(sb.ToString());

            return isValid;
        }

        public void OnCancel()
        {
            View.CloseView();
        }
    }
}
