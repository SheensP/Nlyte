import ListRenderer from "./list-renderer.js";
import DataService from "./data-service.js";

class App {

    constructor() {
        this.selectedBookId = 0;
        this.titlesList = new ListRenderer('titles-list');
        this.wordsList = new ListRenderer('commonWords-list');
        this.searchResults = new ListRenderer('searchResults-list');

        var searchTxt = document.getElementById('searchTxt');
        searchTxt.addEventListener("keyup", this.SearchTxtKeyup.bind(this, searchTxt));

        this.timer;
    }

    go() {
        // TODO
        DataService.GetBooks()
            .catch(ex => {
                console.log('error: ' + ex);
            })
            .then(data => {
                data.forEach(book => {
                    var li = this.titlesList.CreateTitle(book);
                    li.addEventListener('click', this.BookClicked.bind(this, li));
                });
            });
    }

    BookClicked(titleElement) {
        this.selectedBookId = titleElement.id;

        /** HIGHLIGHT BOOK **/
        var allTitles = Array.from(document.getElementById('titles-list').children);
        allTitles.forEach(li => li.classList.remove('selected'))

        titleElement.classList.add('selected');


        /** REFRESH BOOK DETAILS **/
        this.wordsList.Clear();
        DataService.GetTopWords(this.selectedBookId).then(data => {
            data.forEach(wordCount => {
                this.wordsList.CreateWordCount(wordCount);
            });
        });

        /** ENABLE SEARCH **/
        document.getElementById('searchTxt').disabled = false;
    }

    SearchTxtKeyup(searchInput) {
        clearTimeout(this.timer);
        this.timer = setTimeout(() => this.SearchWord(searchInput), 500);
    }

    SearchWord(searchInput) {
        this.searchResults.Clear();
        DataService.WordSearch(this.selectedBookId, searchInput.value).then(data => {
            data.forEach(word => {
                this.searchResults.CreateSearchResult(word);
            });
        });
    }

}
new App().go();