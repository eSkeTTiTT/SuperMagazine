using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SuperMagazine.DAL.Interfaces;
using SuperMagazine.Domain.Entities;

namespace SuperMagazine.DAL.Repositories
{
	public class ProductRepository : IProductRepository
	{
		#region Properties

		private readonly ApplicationDbContext _dbContext = null!;
		private readonly IMemoryCache _memoryCache = null!;

		#endregion

		public ProductRepository(ApplicationDbContext dbContext, IMemoryCache memoryCache)
		{
			_dbContext = dbContext;
			_memoryCache = memoryCache;
		}

		public async Task<List<Product>> GetPoductsByCategoryId(int id) =>
			await _dbContext.Products
				.Where(p => p.CategoryId == id)
				.ToListAsync();
	}
}
