using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperMagazine.Domain.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Вы не ввели логин")]
        [DisplayName("Логин")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Login { get; set; } = null!;

		[Required(ErrorMessage = "Вы не ввели пароль")]
		[DisplayName("Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
    }
}
