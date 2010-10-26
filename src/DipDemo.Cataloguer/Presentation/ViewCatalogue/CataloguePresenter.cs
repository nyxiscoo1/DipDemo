using System;
using DipDemo.Cataloguer.Core;
using DipDemo.Cataloguer.Events;
using DipDemo.Cataloguer.Infrastructure.AppController;
using DipDemo.Cataloguer.Infrastructure.EventAggregator;
using DipDemo.Cataloguer.Infrastructure.Presentation;
using DipDemo.Cataloguer.Infrastructure.Presentation.EventBinding;
using DipDemo.Cataloguer.Requests;

namespace DipDemo.Cataloguer.Presentation.ViewCatalogue
{
    public class CataloguePresenter : AbstractPresenter<ICatalogueView>,
        IEventHandler<CatalogueOpenedEvent>, IEventHandler<BookUpdatedEvent>
    {
        private readonly IMessageBoxCreator msgCreator;
        private readonly IApplicationController appController;
        private readonly Observable<Book> currentBook = new Observable<Book>();

        private Catalogue currentCatalogue;

        public Fact CanEdit
        {
            get { return new Fact(currentBook, () => currentBook.Value != null); }
        }

        public Fact CanRemove
        {
            get { return CanEdit; }
        }

        public CataloguePresenter(ICatalogueView view, IMessageBoxCreator msgCreator, IApplicationController appController)
            : base(view)
        {
            this.msgCreator = msgCreator;
            this.appController = appController;
        }

        public void OnAdd()
        {
            appController.ProcessRequest(new EditBookRequest(new Book()));
        }


        public void OnEdit()
        {
            appController.ProcessRequest(new EditBookRequest(currentBook.Value));
        }

        public void OnRemove()
        {
            if (!msgCreator.AskUser("Вы действтельно хотите удалить книгу [" + currentBook.Value + "]?"))
                return;

            currentCatalogue.RemoveBook(currentBook.Value);
            View.UpdateModel();
        }

        public void ProcessEvent(CatalogueOpenedEvent data)
        {
            currentCatalogue = data.Catalogue;
            currentBook.Value = null;

            View.SetModel(data.Catalogue);
            View.UpdateModel();
        }

        public void OnBooksItemSelected(Book book)
        {
            currentBook.Value = book;
        }

        public void OnBooksItemChoosen(Book book)
        {
            currentBook.Value = book;

            if(currentBook.Value != null)
                OnEdit();
        }

        public void ProcessEvent(BookUpdatedEvent data)
        {
            currentCatalogue.AddBook(data.Book);
            View.UpdateModel();
        }
    }
}