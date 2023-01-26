using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperMagazine.Domain.Models
{
	public class UserProfileRedactorViewModel
	{
		[Required(ErrorMessage = "Поле обязательно к заполнению")]
		[DisplayName("Имя")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "Поле обязательно к заполнению")]
		[DisplayName("Фамилия")]
		public string Surname { get; set; } = null!;

		[Required(ErrorMessage = "Поле обязательно к заполнению")]
		[Range(1, 100, ErrorMessage = "Некорректный возраст")]
		[DisplayName("Возраст")]
		public int Age { get; set; }
	}
}
