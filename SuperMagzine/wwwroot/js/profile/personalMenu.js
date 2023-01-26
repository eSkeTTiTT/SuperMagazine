$(document).ready(function () {
    $('#personal_form').submit(function (e) {
        e.preventDefault();

        alert('lox');
    });
    $('#personal_form').validate({
        rules: {
            personal_name: {
                required: true,
                minlength: 2,
                maxlength: 20,
            },
            personal_surname: {
                required: true,
                minlength: 2,
                maxlength: 30,
            },
            personal_age: {
                required: true,
                range: [1, 100],
            }
        },
        messages: {
            personal_name: {
                required: 'Поле обязательно к заполнению',
                minlength: 'Имя должно содержать от 2 до 20 символов',
                maxlength: 'Имя должно содержать от 2 до 20 символов',
            },
            personal_surname: {
                required: 'Поле обязательно к заполнению',
                minlength: 'Фамилия должна содержать от 2 до 20 символов',
                maxlength: 'Фамилия должна содержать от 2 до 20 символов',
            },
            personal_age: {
                required: 'Поле обязательно к заполнению',
                minlength: 'Некорректный возраст',
                maxlength: 'Некорректный возраст',
            },
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
})