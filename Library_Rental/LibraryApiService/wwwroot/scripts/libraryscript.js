document.addEventListener('DOMContentLoaded', function () {
    let user_id;
    const rentButton = document.getElementById('rentButton');
    const returnButton = document.getElementById('returnButton');

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    user_id = urlParams.get('userId');

    rentButton.addEventListener('click', rentBook);
    returnButton.addEventListener('click', returnBook);

    fetchBooks();
    fetchUserBooks();

    async function fetchUserBooks() {
        try {
            const url = `api/checkout/getbooks?id=${user_id}`;
            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            });
            if (!response.ok) {
                throw new Error('Failed to fetch user books');
            }
            const books = await response.json();
            const bookUserListContainer = document.getElementById('userBookList');
            bookUserListContainer.innerHTML = '';
            books.forEach(book => {
                const li = createUserBookList(book);
                bookUserListContainer.appendChild(li);
            });
        } catch (error) {
            console.error('Error fetching user books:', error);
        }
    }

    async function fetchBooks() {
        try {
            const response = await fetch('api/Library');
            if (!response.ok) {
                throw new Error('Failed to fetch books');
            }
            const books = await response.json();
            const bookListContainer = document.getElementById('bookList');
            bookListContainer.innerHTML = '';
            books.forEach(book => {
                const li = createAvailableBookList(book);
                bookListContainer.appendChild(li);
            });
        } catch (error) {
            console.error('Error fetching books:', error);
        }
    }

    function createAvailableBookList(book) {
        const li = document.createElement('li');
        li.textContent = `${book.book_name} by ${book.book_author}`;
        li.style.cursor = 'pointer';
        li.addEventListener('click', () => {
            deselectPreviousSelections("bookList");
            li.classList.add('highlighted')
            bookClicked(book,"bookList");
            });
        return li;
    }
    function createUserBookList(book) {
        const li = document.createElement('li');
        li.textContent = `${book.book_name}`;
        li.style.cursor = 'pointer';
        li.addEventListener('click', () => {
            deselectPreviousSelections("userBookList");
            li.classList.add('highlighted')
            bookClicked(book, "userBookList")
        });
        return li;
    }
    async function rentBook() {
        const bookContainer = document.getElementById("bookList");
        const book_id = bookContainer.getAttribute('book_id');
        if (!book_id) return;

        try {
            const response = await fetch('api/Checkout', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ checkout_user_id: user_id, checkout_book_id: book_id })
            });

            if (!response.ok) {
                throw new Error('Failed to rent book');
            }

            alert('Book rented successfully!');
            fetchBooks();
            fetchUserBooks();
        } catch (error) {
            console.error('Error renting book:', error);
            alert('Failed to rent book. Please try again.');
        }
    }

    async function returnBook() {
        const bookContainer = document.getElementById("userBookList");
        const book_id = bookContainer.getAttribute('book_id');
        if (!book_id) return;

        try {
            const response = await fetch(`api/checkout/removeBook?user_id=${user_id}&book_id=${book_id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
            });
            if (!response.ok) {
                throw new Error('Failed to return book');
            }
            alert('Book returned successfully!');
            fetchBooks();
            fetchUserBooks();
        } catch (error) {
            console.error('Error returning book:', error);
            alert('Failed to return book. Please try again.');
        }
    }
    function bookClicked(book,str){
        const bookContainer = document.getElementById(str);
        const book_id = bookContainer.setAttribute('book_id',book.book_id);
    }
    function deselectPreviousSelections(list) {
        const bookContainer = document.getElementById(list);
        const selectedBook = bookContainer.querySelector('.highlighted');
        if (selectedBook) {
            selectedBook.classList.remove('highlighted');
        }
    }
});