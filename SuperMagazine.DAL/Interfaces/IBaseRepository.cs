namespace SuperMagazine.DAL.Interfaces
{
	public interface IBaseRepository<T>
	{
		Task<bool> Create(T entity);

		Task<bool> Delete(T entity);

		Task<T> Update(T entity);

		Task<T?> Get(Guid id);

		Task<List<T>> Select();
	}
}
