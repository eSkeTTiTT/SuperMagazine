using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperMagazine.Domain.Entities
{
	public class Category
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public List<Product> Products { get; set; } = new();
	}
}
