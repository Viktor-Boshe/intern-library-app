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
            const user_id = data.user.user_id;
            if (data.success) {
                window.location.href = `Library.html?userId=${user_id}`;
            } else {
                document.getElementById('errorMessage').style.display = 'block';
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