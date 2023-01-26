using Microsoft.AspNetCore.Http;

namespace SuperMagazine.Domain.Models
{
	public class UserProfileViewModel
	{
		public string? Name { get; set; }

		public string? Surname { get; set; }

		public string? Email { get; set; }

		public string? ProfileImageUrl { get; set; }
	}
}
