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
    if (jwtToken) {
        fetch('api/users/validate', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${jwtToken}`
            }
        })
            .then(response => response.json())
            .then(data => {
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

        if (event.submitter.id == 'logIn') {
            const username = document.getElementById('username').value;
            const password = document.getElementById('password').value;
            if (username.trim() === '' || password.trim() === '') {
                document.getElementById('errorMessage').style.display = 'block';
                return;
            }
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
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        }
        else if (event.submitter.id == 'forgotPassword') {
            let username = window.prompt("please enter your username", "");
            if (username != null) {
                fetch(`api/users/user?username=${username}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            let password = window.prompt("please enter a new password", "");
                            if (password) {
                                fetch('/api/users/reset', {
                                    method: 'PUT',
                                    headers: {
                                        'Content-Type': 'application/json'
                                    },
                                    body: JSON.stringify({ username: username, password: password })
                                })
                                    .then(response => response.json())
                                    .then(data => {
                                        if (data.success) {
                                            alert("SUCCESS")
                                            window.location.href = 'FrontPage.html';
                                        } else {
                                            document.getElementById('errorMessage').style.display = 'block';
                                        }
                                    })
                                    .catch(error => {
                                        console.error('Error:', error);
                                    })
                            }
                        }
                        else {
                            document.getElementById('errorMessage').style.display = 'block';
                        }
                    })
            }
        }
        else if (event.submitter.id == 'createUser') {
            window.location.href = 'CreateUserPage.html';
        }
    });
}