using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMagazine.DAL.Interfaces;

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
	}
}
