document.getElementById('CreateUserForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const email = document.getElementById('e-mail').value;
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
        if (email != '') {
            if (typeof (RegExp) == 'function') {
                regX = new RegExp('^([a-zA-Z0-9\\-\\.\\_]+)(\\@)([a-zA-Z0-9\\-\\.]+)(\\.)([a-zA-Z]{2,4})$');
                if (!regX.test(email)) {
                    alert('Please provide a valid e-mail address.');
                    return false;
                };
            }
            createUser(email, username, password);
    }

    function createUser(email,username, password) {
        fetch('/api/users/create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email: email, username: username, password: password })
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