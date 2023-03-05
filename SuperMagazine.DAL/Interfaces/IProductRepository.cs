using SuperMagazine.Domain.Entities;

namespace SuperMagazine.DAL.Interfaces
{
	public interface IProductRepository
	{
		Task<List<Product>> GetPoductsByCategoryId(int id);
	}
}
