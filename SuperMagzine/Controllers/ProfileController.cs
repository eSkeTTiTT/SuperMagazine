using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMagazine.DAL.Interfaces;
using SuperMagazine.Domain.Enums;
using SuperMagazine.Domain.Models;

namespace SuperMagzine.Controllers
{
	public class ProfileController : Controller
	{
		#region Properties

		private UserProfileViewModel? _userProfileViewModel = null;
		private UserProfileRedactorViewModel? _userProfileRedactorViewModel = null;

		private readonly IUserRepository _userRepository;

		#endregion

		#region Constructors

		public ProfileController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		#endregion

		[Authorize]
		[HttpGet]
		public IActionResult Index()
		{
			var userProfile = GetUserProfileViewModel();

			return View(userProfile);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> SavePersonal(UserProfileRedactorViewModel userProfileRedactorViewModel)
		{
			var a = userProfileRedactorViewModel;
			await Task.Delay(5000);
			return Redirect("Index");
		}

		[Authorize]
		[HttpPost]
		public IActionResult GetView(string viewName)
		{
			return viewName switch
			{
				ProfileViews.INDEX => PartialView("Index", GetUserProfileViewModel()),
				ProfileViews.PERSONAL => PartialView("Personal", GetUserProfileRedactorViewModel().Result),
				_ => PartialView("Index", GetUserProfileViewModel()),
			};
		}

		#region Private Methods

		private UserProfileViewModel GetUserProfileViewModel()
		{
			if (_userProfileViewModel is not null)
			{
				return _userProfileViewModel;
			}
			
			var userClaims = HttpContext.User;

			_userProfileViewModel = new UserProfileViewModel
			{
				Name = userClaims.FindFirst(IdentityTypes.Name)?.Value,
				Surname = userClaims.FindFirst(IdentityTypes.Surname)?.Value,
				Email = userClaims.FindFirst(IdentityTypes.Login)?.Value,
				ProfileImageUrl = userClaims.FindFirst(IdentityTypes.ProfileImageUrl)?.Value
			};

			return _userProfileViewModel;
		}

		private async Task<UserProfileRedactorViewModel> GetUserProfileRedactorViewModel()
		{
			if (_userProfileRedactorViewModel is not null)
			{
				return _userProfileRedactorViewModel;
			}

			var userClaims = HttpContext.User;

			string? id = userClaims.FindFirst(IdentityTypes.Id)?.Value;
			Guid guidId = new Guid(id);

			var user = await _userRepository.Get(guidId);

			_userProfileRedactorViewModel = new UserProfileRedactorViewModel
			{
				Name = user.Name,
				Surname = user.Surname,
				Age = user.Age
			};

			return _userProfileRedactorViewModel;
		}

		#endregion
	}
}
