using Library.Models;
using Library.Repositories;
using Library.Data;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    /*
    * 
        BE – Simple command line app that reads in a text file, counts the number of words and returns the most common ten.

        UI – Very simple HTML app which shows how to retrieve some data and display it on the page. 
        i.e. a HTML page including a <script> tag which loads a JS ‘app’ file via ES6 type=”module”.
                        
    */
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static readonly BookRepository _bookRepository = new BookRepository();
        private static readonly BookWordsRepository _wordsRepository = new BookWordsRepository();

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            // Return the list of book ids and titles
            return _bookRepository.GetBooks();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTopWords(int id)
        {
            // Return a list of the 10 most common words (>5 letters) 
            var book = BookStorage.GetBook(id);

            _wordsRepository.Add(book.Content);
            
            return Ok(_wordsRepository.MostCommonWords());
        }

        [HttpGet("{id:int}/count/{word}")]
        public IActionResult WordCount(int id, string word)
        {
            var book = BookStorage.GetBook(id);

            _wordsRepository.Add(book.Content);

            return Ok(_wordsRepository.GetCount(word));
        }

        [HttpGet("{id:int}/search/{query}")]
        public IActionResult SearchForWord(int id, string query)
        {
            var book = BookStorage.GetBook(id);

            _wordsRepository.Add(book.Content);

            return Ok(_wordsRepository.Search(query));
        }

    }
}