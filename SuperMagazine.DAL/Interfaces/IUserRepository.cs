using SuperMagazine.Domain.Entities;

namespace SuperMagazine.DAL.Interfaces
{
	public interface IUserRepository : IBaseRepository<User>
	{
		Task<User?> GetByLoginAndPassword(string login, string password);
	}
}
