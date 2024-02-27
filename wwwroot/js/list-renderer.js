class ListRenderer {
    ulElement;
    constructor(ulId) {
        this.ulElement = document.getElementById(ulId);
    }

    CreateTitle(book) {

        var li = document.createElement("li");
        li.setAttribute("id", book.id);
        li.innerText = book.title;

        this.ulElement.appendChild(li);

        return li;
    }

    CreateWordCount(wordCount) {

        var li = document.createElement("li");
        li.setAttribute("id", wordCount.word);
        li.innerHTML = wordCount.word + '<div class="count">' + wordCount.count + '<div>';

        this.ulElement.appendChild(li);

        return li;
    }

    CreateSearchResult(wordCount) {

        var li = document.createElement("li");
        li.innerText = wordCount.word;

        this.ulElement.appendChild(li);

        return li;
    }

    Clear() {
        this.ulElement.innerHTML = '';
    }
}
export default ListRenderer;