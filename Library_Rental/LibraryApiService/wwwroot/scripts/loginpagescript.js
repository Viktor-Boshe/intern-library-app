document.getElementById('loginForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    fetch('/api/users/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username: username, password: password })
    })
        .then(response => response.json())
        .then(data => {
            token = data.token;
            if (data.success) {
                window.location.href = `Library.html?user=${token}`;
            } else {
                document.getElementById('errorMessage').style.display = 'block';
                alert("ERROR")
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
});

document.getElementById('CreateUserForm').addEventListener('submit', function (event) {
    event.preventDefault();
    window.location.href = 'CreateUserPage.html';
});