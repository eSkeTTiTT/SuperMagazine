using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SuperMagazine.DAL.Interfaces;
using SuperMagazine.Domain.Entities;

namespace SuperMagazine.DAL.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		#region Properties

		private readonly ApplicationDbContext _dbContext = null!;
		private readonly IMemoryCache _memoryCache = null!;

		#endregion

		public CategoryRepository(ApplicationDbContext dbContext, IMemoryCache memoryCache)
		{
			_dbContext = dbContext;
			_memoryCache = memoryCache;
		}

		public async Task<Category?> GetById(int id) =>
			await _dbContext.Categories.FirstOrDefaultAsync(v => v.Id == id);

		public async Task<List<Category>> Select() =>
			await _dbContext.Categories.ToListAsync();
	}
}
