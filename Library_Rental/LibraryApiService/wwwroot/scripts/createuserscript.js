document.getElementById('CreateUserForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    if (event.submitter.id == 'logIn') {
        logInPage();
    }
    else {
        if (!username || !password) {
            alert("Username and password are required.");
            document.getElementById('username').value = '';
            document.getElementById('password').value = '';
            return;
        }
        createUser(username, password);
    }

    function createUser(username, password) {
        fetch('/api/users/create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ username: username, password: password })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    window.location.href = 'FrontPage.html';
                } else {
                    document.getElementById('errorMessage').style.display = 'block';
                }
            })
            .catch(error => {
                console.error('Error:', error);
            })
    }
    function logInPage() {
        window.location.href = 'FrontPage.html';
    }
})