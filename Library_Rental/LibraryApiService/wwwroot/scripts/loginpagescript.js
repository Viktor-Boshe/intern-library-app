function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

window.addEventListener('DOMContentLoaded', function () {
    validateToken();
});

function validateToken() {
    const jwtToken = getCookie('jwtToken');
    console.log(jwtToken);
    if (jwtToken) {
        console.log("in");
        fetch('api/users/validate', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${jwtToken}`
            }
        })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                if (data.success) {
                    document.cookie = `jwtToken=${data.token}; path=/; secure; SameSite=Strict;`;
                    window.location.href = `Library.html`;
                } else {
                    handleLogin();
                }
            })
            .catch(error => {
                console.error('Error validating token:', error);
                handleLogin();
            });
    } else {
        handleLogin();
    }
}

function handleLogin() {
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
                if (data.success) {
                    document.cookie = `jwtToken=${data.token}; path=/; secure; SameSite=Strict;`;
                    window.location.href = `Library.html`;
                } else {
                    document.getElementById('errorMessage').style.display = 'block';
                    alert("ERROR");
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });
}

document.getElementById('CreateUserForm').addEventListener('submit', function (event) {
    event.preventDefault();
    window.location.href = 'CreateUserPage.html';
});