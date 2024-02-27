class DataService {

    static apiRoot = 'api/Books';

    static GetBooks() {
        return this.fetchJson('');
    }

    static GetTopWords(id) {
        return this.fetchJson('/' + id);
    }

    static WordCount(id, word) {
        return this.fetchJson('/' + id + '/count/' + word);
    }

    static WordSearch(id, query) {
        return this.fetchJson('/' + id + '/search/' + query);
    }

    static fetchJson(path) {
        return fetch(this.apiRoot + path).then((response) => {
            if( !response.ok )
                throw response.statusText;
            return response.json();
        }); 
    }
}
export default DataService;