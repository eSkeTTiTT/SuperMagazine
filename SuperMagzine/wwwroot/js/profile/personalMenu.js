$(document).ready(function () {
    let getName = () => document.getElementById('personal_name').value;
    let getSurname = () => document.getElementById('personal_surname').value;
    let getAge = () => document.getElementById('personal_age').value;

    let _nameOld = getName();
    let _surnameOld = getSurname();
    let _ageOld = getAge();

    let _nameNew = null;
    let _surnameNew = null;
    let _ageNew = null;

    $('#personal_form').validate({
        rules: {
            Name: {
                required: true,
                minlength: 2,
                maxlength: 20,
            },
            Surname: {
                required: true,
                minlength: 2,
                maxlength: 30,
            },
            Age: {
                required: true,
                range: [1, 100],
            }
        },
        messages: {
            Name: {
                required: 'Поле обязательно к заполнению',
                minlength: 'Имя должно содержать от 2 до 20 символов',
                maxlength: 'Имя должно содержать от 2 до 20 символов',
            },
            Surname: {
                required: 'Поле обязательно к заполнению',
                minlength: 'Фамилия должна содержать от 2 до 20 символов',
                maxlength: 'Фамилия должна содержать от 2 до 20 символов',
            },
            Age: {
                required: 'Поле обязательно к заполнению',
                range: 'Некорректный возраст',
            }
        },
        submitHandler: function (form) {
            _nameNew = getName();
            _surnameNew = getSurname();
            _ageNew = getAge();

            let isCorrect = !(_nameNew === _nameOld
                && _surnameNew === _surnameOld
                && _ageNew === _ageOld);

            if (isCorrect) {
                $.blockUI({
                    message: '<div class="loader"></div>',
                    css: {
                        border: 'none',
                        backgroundColor: 'transparent',
                        padding: '15px',
                    }
                });

                let serializeFormData = $('#personal_form').serialize();

                $.ajax({
                    url: '/profile/savepersonal',
                    method: 'post',
                    data: serializeFormData,
                    success: function (response) {
                        alert('Данные успешно сохранены!');
                        $.unblockUI();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert('Ошибка на сервере: ' + textStatus + ' ' + errorThrown);
                        $.unblockUI();
                    }
                });
            }
            else {
                alert('Вы ничего не изменили.');
            }
        }
    });
});