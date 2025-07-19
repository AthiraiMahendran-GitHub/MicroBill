
$(document).ready(function () {

    // Toggle between Login/Register
    $('#showRegister').on('click', function () {
        $('#divLoginSection').hide();
        $('#divRegisterSection').show();
    });

    $('#showLogin').on('click', function () {
        $('#divRegisterSection').hide();
        $('#divLoginSection').show();
    });

    // Show/Hide Password
    $('.input-group-text').on('click', function () {
        const passwordInput = $(this).closest('.input-group').find('input');
        const icon = $(this).find('i');
        const type = passwordInput.attr('type') === 'password' ? 'text' : 'password';
        passwordInput.attr('type', type);
        icon.toggleClass('ri-eye-off-line ri-eye-line');
    });

    // AJAX Login
    $('#loginForm').submit(function (e) {
        e.preventDefault();

        let email = $('#lemail').val();
        let passwd = $('#lpassword').val();
        if (email != '' && passwd != '') {
            $.ajax({
                type: 'POST',
                url: '/Login/Login',
                dataType: 'JSON',
                data: {
                    email: $('#lemail').val(),
                    password: $('#lpassword').val()
                },
                success: function (response) {
                    if (response == true) {
                        //toastr.success("Login successful!");
                        //alert("Login successful!");
                        window.location.href = '/Home/Dashboard';
                    } else {
                       // toastr.error("Invalid UserName or Password");
                        alert("Invalid UserName or Password");
                    }
                },
                error: function(){
                    toastr.error("Error");
                }

            });
        }
        else {
            alert("Please enter login Credentials");
        }
    });

    // AJAX Register
    $('#registerForm').submit(function (e) {
        e.preventDefault();

        $.ajax({
            type: 'POST',
            url: '/Login/Register',
            data: {
                username: $('#username').val(),
                email: $('#remail').val(),
                password: $('#rpassword').val()
            },
            success: function (response) {
                if (response.success == true) {
                    toastr.success('Registration successful. Please login.');
                    $('#showLogin').click();
                } else {
                    toastr.error(response.message);
                }
            }
        });
    });

    // Remember Me Handling
    $('#remember-me').change(function () {
        if (this.checked) {
            localStorage.setItem("rememberEmail", $('#lemail').val());
            localStorage.setItem("rememberPswd", $('#lpassword').val());
        } else {
            localStorage.removeItem("rememberEmail");
        }
    });

    // Load Remembered Email
    const savedEmail = localStorage.getItem("rememberEmail");
    if (savedEmail) {
        $('#lemail').val(savedEmail);
        $('#remember-me').prop('checked', true);
    }

});
