using Microsoft.AspNetCore.Mvc;
using SuperMagazine.Domain.Consts;
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

			return View(model);
		}

		#endregion

	}
}
