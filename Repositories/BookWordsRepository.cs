using Library.Data;

namespace Library.Models
{
    // Stores the words in a book. These should always be returned in "Capital" case. All matching should be case-insensitive (e.g. searching "cap" would find "Capital")
    public class BookWordsRepository
    {
        List<WordCount> _wordCounts = new List<WordCount>();

        // Add words parsed from the given text into this repository
        public void Add(string text)
        {
            var minusPunctuation = new string(text.Select(c => char.IsPunctuation(c) ? ' ' : c).ToArray());

            var results = minusPunctuation.Split()
                .Select(x => x.Trim())
                .Where(x => !String.IsNullOrEmpty(x) && x.Length >= 3)
                .GroupBy(x => x.ToLower())
                .Select(x => new WordCount()
                {
                    Word = x.Key.Substring(0, 1).ToUpper() + x.Key.Substring(1).ToLower(),
                    Count = x.Count()
                }
            ).ToList();

            _wordCounts = results;
        }

        // Return the number of appearances of a specified word in this book
        public int GetCount(string word)
        {
            var wordCount = _wordCounts
                .Where(x => String.Equals(x.Word, word, StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault();

            return wordCount == null ? 0 : wordCount.Count;
        }

        // Return a list of words which start with the specified prefix in this book
        public List<WordCount> Search(string query)
        {
            return _wordCounts
                .Where(x => x.Word.StartsWith(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Return the top-10 most common words in this book, along with their counts, in descending order of appearance.
        public List<WordCount> MostCommonWords()
        {
            return _wordCounts
                   .Where(x => x.Word.Length >= 5)
                   .OrderByDescending(x => x.Count)
                   .Take(10)
                   .ToList();
        }

    }
}