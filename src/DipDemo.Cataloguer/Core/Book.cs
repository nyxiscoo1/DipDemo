using System;

namespace DipDemo.Cataloguer.Core
{
    [Serializable]
    public class Book
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public Book()
        {
        }

        public Book(string name, string author, string description)
        {
            Name = name;
            Author = author;
            Description = description;
        }
    }
}