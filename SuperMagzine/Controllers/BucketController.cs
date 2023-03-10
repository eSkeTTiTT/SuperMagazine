using Microsoft.AspNetCore.Mvc;
using SuperMagazine.Domain.Consts;
using SuperMagazine.Domain.Entities;
using SuperMagazine.Domain.Models.Catalog;
using SuperMagazine.Extensions.Session;

namespace SuperMagzine.Controllers
{
	public class BucketController : Controller
	{
		#region Properties



		#endregion

		#region Constructor

		public BucketController()
		{

		}

		#endregion

		#region Methods

		[HttpGet]
		public IActionResult Index()
		{
			var model = HttpContext.Session.Get<List<BucketViewModel>>(Constants.SESSION_BUCKET_LIST);

			return View(model ?? new());
		}

		[HttpPost]
		public JsonResult PutItemIntoBucket(Product product)
		{
			var session = HttpContext.Session;
			var bucketList = session.Get<List<BucketViewModel>>(Constants.SESSION_BUCKET_LIST);
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
			session.Set(Constants.SESSION_COUNT, bucketList?.Count);

			return Json(new { bucketList?.Count });
		}

		[HttpPost]
		public IActionResult RemoveItemFromBucket(int id)
		{
			var session = HttpContext.Session;
			var bucketList = session.Get<List<BucketViewModel>>(Constants.SESSION_BUCKET_LIST);

			var deleteItem = bucketList?.Single(v => v.Id == id);

			if (deleteItem != null)
			{
				bucketList?.Remove(deleteItem);
			}

			session.Set(Constants.SESSION_BUCKET_LIST, bucketList);
			session.Set(Constants.SESSION_COUNT, bucketList?.Count);

			return View(viewName: "Index", model: bucketList);
		}

		#endregion

	}
}
