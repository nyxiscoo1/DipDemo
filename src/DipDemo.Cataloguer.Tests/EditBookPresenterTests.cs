using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DipDemo.Cataloguer.Presentation.EditBook;
using DipDemo.Cataloguer.Core;
using Rhino.Mocks;
using DipDemo.Cataloguer.Infrastructure.Presentation;
using DipDemo.Cataloguer.Infrastructure.AppController;
using DipDemo.Cataloguer.Events;

namespace DipDemo.Cataloguer.Tests
{
    [TestFixture]
    public class EditBookPresenterTests : AutoMockingTestFixture<EditBookPresenter>
    {
        protected Book book;

        protected IEditBookView View
        {
            get { return Mock.Get<IEditBookView>(); }
        }

        protected override void SharedContext()
        {
            View.Stub(x => x.Name).PropertyBehavior();
            View.Stub(x => x.Author).PropertyBehavior();
            View.Stub(x => x.Description).PropertyBehavior();

            book = new Book("TestBook", "Author", "Description");
            SUT.Execute(new Requests.EditBookRequest(book));

            Mock.Get<IEditBookView>().AssertWasCalled(x => x.ShowModalView());

            Assert.AreEqual(book.Name, View.Name);
            Assert.AreEqual(book.Author, View.Author);
            Assert.AreEqual(book.Description, View.Description);
        }

        [TestFixture]
        public class When_saving_and_entered_data_is_invalid : EditBookPresenterTests
        {
            protected override void Context()
            {
                View.Author = string.Empty;
                View.Name = string.Empty;

                SUT.OnSave();
            }

            [Test]
            public void Should_show_error_message()
            {
                Mock.Get<IMessageBoxCreator>().AssertWasCalled(x => x.ShowError(null), x => x.IgnoreArguments());
            }
        }

        [TestFixture]
        public class When_saving_and_entered_data_is_valid : EditBookPresenterTests
        {
            protected override void Context()
            {
                View.Name = "NewName";
                View.Author = "NewAuthor";
                View.Description = "NewDescription";

                SUT.OnSave();
            }

            [Test]
            public void Should_clsoe_view()
            {
                View.AssertWasCalled(x => x.CloseView());
            }

            [Test]
            public void Should_not_raise_updated_event()
            {
                Mock.Get<IApplicationController>().AssertWasCalled(x => x.Raise(Arg<BookUpdatedEvent>.Matches(a => a.Book == book)));
            }

            [Test]
            public void Should_copy_new_values_to_book_from_view()
            {
                Assert.AreEqual("NewName", book.Name);
                Assert.AreEqual("NewAuthor", book.Author);
                Assert.AreEqual("NewDescription", book.Description);
            }
        }
    }
}
