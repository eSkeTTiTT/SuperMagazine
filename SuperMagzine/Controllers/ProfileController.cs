using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMagazine.DAL.Interfaces;
using SuperMagazine.Domain.Enums;
using SuperMagazine.Domain.Models;
using SuperMagazine.Services.Interfaces;

namespace SuperMagzine.Controllers
{
	public class ProfileController : Controller
	{
		#region Properties

		private readonly IUserRepository _userRepository;
		private readonly IUserAuthorizationService _authorizationService;

		#endregion

		#region Constructors

		public ProfileController(IUserRepository userRepository, IUserAuthorizationService userAuthorization)
		{
			_userRepository = userRepository;
			_authorizationService = userAuthorization;
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
		public async Task<IActionResult> SavePersonal(UserProfileRedactorViewModel userRedactorVM)
		{
			// Сохраняем в бд
			var userClaims = HttpContext.User;
			Guid id = new Guid(userClaims.FindFirst(IdentityTypes.Id)?.Value ?? "");
			var result = await _userRepository.Get(id);

			if (result != null)
			{
				result.Surname = userRedactorVM.Surname;
				result.Name = userRedactorVM.Name;
				result.Age = userRedactorVM.Age;

				await _userRepository.Update(result);

				// Пересоздаем claims
				var updateResult = await _authorizationService.Update(result, HttpContext);
			}
			else
			{
				return BadRequest();
			}

			return Ok();
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
			var userClaims = HttpContext.User;

			return new UserProfileViewModel
			{
				Name = userClaims.FindFirst(IdentityTypes.Name)?.Value,
				Surname = userClaims.FindFirst(IdentityTypes.Surname)?.Value,
				Email = userClaims.FindFirst(IdentityTypes.Login)?.Value,
				ProfileImageUrl = userClaims.FindFirst(IdentityTypes.ProfileImageUrl)?.Value
			};
		}

		private async Task<UserProfileRedactorViewModel> GetUserProfileRedactorViewModel()
		{
			var userClaims = HttpContext.User;

			string? id = userClaims.FindFirst(IdentityTypes.Id)?.Value;
			Guid guidId = new Guid(id);

			var user = await _userRepository.Get(guidId);

			return new UserProfileRedactorViewModel
			{
				Name = user.Name,
				Surname = user.Surname,
				Age = user.Age
			};
		}

		#endregion
	}
}
