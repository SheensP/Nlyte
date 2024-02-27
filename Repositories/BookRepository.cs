using Library.Data;
using Library.Models;
using System.Text.RegularExpressions;

namespace Library.Repositories
{

    public class BookRepository
    {
        // Read the list of book files in the Resources folder and allow callers to retrieve a repository of the words for each book
        // NOTE: Avoid re-reading files on each request.

        const string _CONTENTS_PREFIX = "Contents";

        public BookRepository() { }

        public List<Book> GetBooks()
        {
            if (BookStorage.Books?.Count > 0) { return BookStorage.Books; }

            BookStorage.Books = new List<Book>();

            string root = Directory.GetCurrentDirectory();
            string bookFolder = "Resources";

            // Validate directory
            string bookDirectory = Path.Combine(root, bookFolder);
            if (!Directory.Exists(bookDirectory)) { throw new DirectoryNotFoundException("Book directory not found"); }

            // Get files
            string[] filesPaths = Directory.GetFiles(bookDirectory, "*.txt");
            if (filesPaths.Length <= 0) { throw new Exception("No books found"); }

            foreach (string filePath in filesPaths)
            {
                BookStorage.Books.Add(ReadBookFile(filePath));
            }

            return BookStorage.Books;
        }

        private Book ReadBookFile(string filePath)
        {
            var result = new Book();

            var fileContent = File.ReadAllText(filePath);

            // Get title
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var fileTitleSplit = fileName.Split("-");
            int bookId = 0;

            int.TryParse(fileTitleSplit[0], out bookId);
            string bookTitle = fileTitleSplit[1].Trim();

            // Get book content
            string contentExpression = $@"{_CONTENTS_PREFIX}(.*)";
            Regex contentRegex = new Regex(contentExpression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match contentMatch = contentRegex.Match(fileContent);

            string content = string.Empty;
            if (contentMatch.Success)
            {
                content = contentMatch.Groups[1].Value.Trim();
            }

            return new Book()
            {
                Id = bookId,
                Title = bookTitle,
                Content = content
            };
        }

    }


}