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

		#region Operators

		public static bool operator ==(UserProfileRedactorViewModel first, UserProfileRedactorViewModel second) =>
			first.Name == second.Name
			&& first.Surname == second.Surname
			&& first.Age == second.Age;

		public static bool operator !=(UserProfileRedactorViewModel first, UserProfileRedactorViewModel second) =>
			!(first == second);

		#endregion
	}
}
