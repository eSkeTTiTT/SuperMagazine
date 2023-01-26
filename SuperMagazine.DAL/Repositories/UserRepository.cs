using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SuperMagazine.DAL.Interfaces;
using SuperMagazine.Domain.Entities;

namespace SuperMagazine.DAL.Repositories
{
	public class UserRepository :BaseCacheRepository<User>, IUserRepository
	{
		#region Properties

		private readonly ApplicationDbContext _dbContext = null!;
		private readonly IMemoryCache _memoryCache = null!;

		#endregion

		#region Constructors

		public UserRepository(ApplicationDbContext dbContext, IMemoryCache memoryCache)
		{
			_dbContext = dbContext;
			_memoryCache = memoryCache;
		}

		#endregion

		#region Methods

		public async Task<bool> Create(User entity)
		{
			_memoryCache.TryGetValue(entity.Id, out User? user);

			if (user is null)
			{
				if (await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == entity.Login) is not null)
				{
					return false;
				}

				await _dbContext.Users.AddAsync(entity);
				await _dbContext.SaveChangesAsync();

				AddCache(entity);

				return true;
			}

			return false;
		}

		public async Task<bool> Delete(User entity)
		{
			_dbContext.Users.Remove(entity);
			await _dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<User?> Get(Guid id)
		{
			_memoryCache.TryGetValue(id, out User? user);

			if (user is null)
			{
				var existUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
				if (existUser is not null)
				{
					AddCache(existUser);
				}

				return existUser;
			}

			return user;
		}

		public async Task<User?> GetByLoginAndPassword(string login, string password) =>
			await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);


		public async Task<List<User>> Select() =>
			await _dbContext.Users.ToListAsync();

		public async Task<User> Update(User entity)
		{
			_dbContext.Users.Update(entity);
			await _dbContext.SaveChangesAsync();

			AddCache(entity);

			return entity;
		}

		protected override void AddCache(User entity)
		{
			_memoryCache.Set(entity.Id, entity, CacheOptions);
		}

		#endregion
	}
}
