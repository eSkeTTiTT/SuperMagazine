using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMagazine.DAL.Interfaces;
using SuperMagazine.Domain.Consts;
using SuperMagazine.Domain.Entities;
using SuperMagazine.Domain.Models.Catalog;
using SuperMagazine.Extensions.Session;

namespace SuperMagzine.Controllers
{
	[Authorize]
	public class CatalogController : Controller
	{
		#region Constructors

		public CatalogController(ICategoryRepository categoryRepository, IProductRepository productRepository)
		{
			_categoryRepository = categoryRepository;
			_productRepository = productRepository;
		}

		#endregion

		#region Properties

		private readonly ICategoryRepository _categoryRepository;
		private readonly IProductRepository _productRepository;

		#endregion

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var categories = await _categoryRepository.Select();

			return View(model: categories);
		}

		[HttpPost]
		public async Task<IActionResult> Index(int id)
		{
			var products = await _productRepository.GetPoductsByCategoryId(id);

			return View("Products", model: products);
		}

		[HttpPost]
		public async Task<JsonResult> PutIntoBucket(Product product)
		{
			var session = HttpContext.Session;
            var bucketList = session.Get<List<BucketViewModel>>(Constants.SESSION_BUCKET_LIST) ?? new();
			var bucketItem = bucketList?.FirstOrDefault(v => v.Id == product.Id);

            if (bucketItem != null)
			{
				bucketItem.Amount += 1;
			}
			else
			{
				bucketItem = new()
				{
                    Id = product.Id,
                    Name = product.Name,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Amount = 1
                };

				bucketList?.Add(bucketItem);
			}

            session.Set(Constants.SESSION_BUCKET_LIST, bucketList);
            session.Set<int?>(Constants.SESSION_COUNT, bucketList?.Count);

            return Json(new { bucketList?.Count });
		}
	}
}
