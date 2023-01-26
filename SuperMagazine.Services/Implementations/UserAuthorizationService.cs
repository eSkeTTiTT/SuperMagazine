using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using SuperMagazine.DAL.Interfaces;
using SuperMagazine.Domain.Entities;
using SuperMagazine.Domain.Enums;
using SuperMagazine.Domain.Helpers;
using SuperMagazine.Domain.Models;
using SuperMagazine.Services.Interfaces;
using System.Security.Claims;

namespace SuperMagazine.Services.Implementations
{
	public class UserAuthorizationService : IUserAuthorizationService
	{
		#region Properties

		private readonly IUserRepository _userRepository;
		private readonly IPhotoService _photoService;

		#endregion

		#region Constructors

		public UserAuthorizationService(IUserRepository userRepository,
			IPhotoService photoService)
		{
			_userRepository = userRepository;
			_photoService = photoService;
		}

		#endregion

		public async Task<bool> Authentication(UserLoginViewModel userLogin, HttpContext context)
		{
			string hashPassword = HashHelper.HashPassword(userLogin.Password);
			User? user = await _userRepository.GetByLoginAndPassword(userLogin.Login, hashPassword);

			if (user is null)
			{
				return false;
			}

			bool isClaimsCreated = await CreateClaims(user, context);

			if (!isClaimsCreated)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> Register(UserRegisterViewModel userRegister)
		{
			var imageUploadResult = await _photoService.AddPhotoAsync(userRegister.Image);
			string url = imageUploadResult.Url.ToString();

			var user = CreateUser(userRegister, url);

			bool isCreated = await _userRepository.Create(user);

			if (isCreated == false)
			{
				return false;
			}

			return true;
		}

		#region Private Methods

		private async Task<bool> CreateClaims(User user, HttpContext context)
		{
			try
			{
				var claims = new List<Claim>
				{
					new Claim(IdentityTypes.Id, user.Id.ToString()),
					new Claim(IdentityTypes.Login, user.Login),
					new Claim(IdentityTypes.Name, user.Name),
					new Claim(IdentityTypes.Surname, user.Surname),
					new Claim(IdentityTypes.ProfileImageUrl, user.ProfileImageUrl)
				};

				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
				await context.SignInAsync(claimsPrincipal);
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}

		private User CreateUser(UserRegisterViewModel userRegister, string url) => 
			new()
			{
				Name = userRegister.UserName,
				Surname = userRegister.UserSurname,
				Age = userRegister.Age,
				Login = userRegister.Email,
				Password = HashHelper.HashPassword(userRegister.Password),
				ProfileImageUrl = url,
				Role = Role.User
			};

		#endregion
	}
}
