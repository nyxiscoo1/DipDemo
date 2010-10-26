using System;
using System.Collections.Generic;

namespace DipDemo.Cataloguer.Core
{
    [Serializable]
    public class Catalogue
    {
        private IList<Book> books = new List<Book>();
        private string oldName;
        private bool isCollectionChanged = false;

        public string Name { get; set; }

        public string FileName { get; set; }

        public IEnumerable<Book> Books
        {
            get { return books; }
        }

        public bool IsDirty
        {
            get { return isCollectionChanged || oldName != Name; }
        }

        public Catalogue()
        {
        }

        public Catalogue(string fileName, string name)
        {
            FileName = fileName;
            Name = name;

            ResetDirty();
        }

        public void AddBook(Book book)
        {
            isCollectionChanged = true;
            if (books.Contains(book))
                return;

            books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            isCollectionChanged = true;
            books.Remove(book);
        }

        public void ResetDirty()
        {
            isCollectionChanged = false;
            oldName = Name;
        }

        public ValidationResult Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return new ValidationResult(false, new[] { "Имя не должно быть пустым" });
            }

            return new ValidationResult();
        }
    }
}
