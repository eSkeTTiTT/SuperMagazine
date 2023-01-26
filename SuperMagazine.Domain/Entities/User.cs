using SuperMagazine.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMagazine.Domain.Entities
{
	public class User
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid Id { get; set; }

		public string Name { get; set; } = null!;

		public string Surname { get; set; } = null!;

		public string Login { get; set; } = null!;

		public string Password { get; set; } = null!;

		public int Age { get; set; }

		public string ProfileImageUrl { get; set; } = null!;

		public Role Role { get; set; }
	}
}