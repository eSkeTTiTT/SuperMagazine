using Microsoft.AspNetCore.Mvc;
using SuperMagazine.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SuperMagazine.Domain.Models;
using SuperMagazine.Domain.Enums;

namespace SuperMagzine.Controllers
{
	public class UserController : Controller
	{
		#region Properties

		private readonly IUserAuthorizationService _authorizationService;
		private readonly IPhotoService _photoService;

		#endregion

		#region Constructor

		public UserController(IUserAuthorizationService authorizationService,
			IPhotoService photoService)
		{
			_authorizationService = authorizationService;
			_photoService = photoService;
		}

		#endregion

		#region Login

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginViewModel user)
		{
			if (ModelState.IsValid)
			{
				bool isAuth = await _authorizationService.Authentication(user, HttpContext);

				if (isAuth)
				{
					return Redirect("/home/index");
				}

				ModelState.AddModelError("", "Введен неверный логин или пароль");
			}

			return View(user);
		}

		#endregion

		#region Register

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterViewModel user)
		{
			if (ModelState.IsValid)
			{
				bool isCreated = await _authorizationService.Register(user);

				if (isCreated)
				{
					return RedirectToAction("Login");
				}

				ModelState.AddModelError("", "Пользователь с таким логином уже существует");
			}

			return View(user);
		}

		#endregion
	}
}