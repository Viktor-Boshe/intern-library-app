document.addEventListener('DOMContentLoaded', function () {
    let user_id;
    const rentButton = document.getElementById('rentButton');
    const returnButton = document.getElementById('returnButton');
    const bookNameInput = document.getElementById('book_name_input');
    const bookAuthorInput = document.getElementById('book_author_input');
    const bookDescriptionInput = document.getElementById('book_description_input');
    const searchButton = document.getElementById('searchButton');
    const refreshingInterval = 15;

    searchButton.addEventListener('click', searchBooks);
    function getCookie(name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
    }
    token = getCookie('jwtToken');

    rentButton.addEventListener('click', rentBook);
    returnButton.addEventListener('click', returnBook);

    fetchBooks();
    fetchUserBooks();

    async function fetchUserBooks() {
        try {
            await refreshTokenIfNeeded();

            const url = `api/checkout/getbooks`;
            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
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
            await refreshTokenIfNeeded();

            const response = await fetch('api/Library', {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
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
        await refreshTokenIfNeeded();

        const bookContainer = document.getElementById("bookList");
        const book_id = bookContainer.getAttribute('book_id');
        if (!book_id) return;

        try {
            const response = await fetch(`api/Checkout?user=book_id=${book_id}`, {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
            });

            if (!response.ok) {
                const errorResponse = await response.json();
                alert('failed to rent book, ' + errorResponse.error);
                throw new Error(errorResponse.error);
            }

            alert('Book rented successfully!');
            fetchBooks();
            fetchUserBooks();
        } catch (error) {
            console.error('failed to rent book',error);
        }
    }

    async function returnBook() {
        await refreshTokenIfNeeded();

        const bookContainer = document.getElementById("userBookList");
        const book_id = bookContainer.getAttribute('book_id');
        if (!book_id) return;

        try {
            const response = await fetch(`api/checkout/removeBook?book_id=${book_id}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
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
        const book_id = bookContainer.setAttribute('book_id', book.book_id);
        document.getElementById("ItemPreview").src = atob(book.book_coverImg);
        document.getElementById("DescriptionPreview").textContent = book.book_description;
    }
    function deselectPreviousSelections(list) {
        const bookContainer = document.getElementById(list);
        const selectedBook = bookContainer.querySelector('.highlighted');
        if (selectedBook) {
            selectedBook.classList.remove('highlighted');
        }
    }
    async function searchBooks() {
        try {
            await refreshTokenIfNeeded();

            const response = await fetch('https://knigoprebaruvac.com/api/search', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    book_name: bookNameInput.value,
                    book_author: bookAuthorInput.value,
                    book_description: bookDescriptionInput.value
                })
            });

            if (!response.ok) {
                throw new Error('Failed to fetch books');
            }

            const bookIds = await response.json();
            const books = await fetchBooksByIds(bookIds);
            const bookListContainer = document.getElementById('bookList');
            bookListContainer.innerHTML = '';
            books.forEach(book => {
                const li = createAvailableBookList(book);
                bookListContainer.appendChild(li);
            });
        } catch (error) {
            console.error('Error searching books:', error);
        }
    }

    async function fetchBooksByIds(bookIds) {
        try {
            await refreshTokenIfNeeded();

            const response = await fetch('api/Library/GetBooksByIds', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(bookIds)
            });

            if (!response.ok) {
                throw new Error('Failed to fetch books by IDs');
            }

            const books = await response.json();
            return books;
        } catch (error) {
            console.error('Error fetching books by IDs:', error);
            throw error;
        }
    }
    async function refreshTokenIfNeeded() {
        const currentTime = Math.floor(Date.now() / 1000);
        const tokenExpiration = parseJwt(token).exp;
        const timeRemaining = tokenExpiration - currentTime;
        console.log(timeRemaining);

        if (timeRemaining < refreshingInterval) {
            try {
                const response = await fetch('api/users/refresh', {
                    method: 'POST',
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });
                if (response.ok) {
                    const data = await response.json();
                    token = data.token;
                    document.cookie = `jwtToken=${data.token}; path=/; secure; SameSite=Strict;`;
                } else {
                    alert("Session expired. Please log in again.");
                    window.location.href = 'FrontPage.html';
                }
            } catch (error) {
                console.error('Error refreshing token:', error);
            }
        }
    }

    function parseJwt(token) {
        var base64Url = token.split('.')[1];
        var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(jsonPayload);
    }

});