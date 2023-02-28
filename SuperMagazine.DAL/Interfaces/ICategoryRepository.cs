using SuperMagazine.Domain.Entities;

namespace SuperMagazine.DAL.Interfaces
{
	public interface ICategoryRepository
	{
		Task<Category?> GetById(int id);
		Task<List<Category>> Select();
	}
}
