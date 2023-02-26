using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperMagazine.Domain.Entities
{
	public class Product
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }

		public int CategoryId { get; set; }
		public Category Category { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

		public string Description { get; set; } = null!;

		public decimal Price { get; set; }
	}
}
