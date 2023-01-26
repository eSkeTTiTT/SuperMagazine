using Microsoft.AspNetCore.Http;
using SuperMagazine.Domain.Attributes.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperMagazine.Domain.Models
{
	public class UserRegisterViewModel
	{
		[Required(ErrorMessage = "Вы не ввели имя")]
		[DisplayName("Имя")]
		public string UserName { get; set; } = null!;

		[Required(ErrorMessage = "Вы не ввели фамилию")]
		[DisplayName("Фамилия")]
		public string UserSurname { get; set; } = null!;

		[Required(ErrorMessage = "Вы не ввели пароль")]
		[DisplayName("Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[Required(ErrorMessage = "Вы не ввели пароль")]
		[Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
		[DisplayName("Повторите пароль")]
		[DataType(DataType.Password)]
		public string RepeatPassword { get; set; } = null!;

		[Required(ErrorMessage = "Вы не ввели логин")]
		[DisplayName("Логин")]
		[EmailAddress(ErrorMessage = "Некорректный адрес")]
		public string Email { get; set; } = null!;

		[Required(ErrorMessage = "Вы не ввели возраст")]
		[Range(1, 100, ErrorMessage = "Некорректный возраст")]
		[DisplayName("Возраст")]
		public int Age { get; set; }

		[Required(ErrorMessage = "Вы не указали изображение")]
		[DisplayName("Изображение")]
		[FileContentType("image/jpeg, image/png")]
		public IFormFile Image { get; set; } = null!;
	}
}
