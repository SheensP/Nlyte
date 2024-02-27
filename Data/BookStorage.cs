using Library.Models;
using System;


namespace Library.Data
{
    public static class BookStorage
    {
        public static List<Book> Books { get; set; }
        public static Book GetBook(int bookId)
        {
            if (BookStorage.Books == null) { throw new ArgumentNullException("Books have not been loaded"); }

            return BookStorage.Books.Where(x => x.Id == bookId).Single();
        }

    }
}