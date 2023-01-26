using Microsoft.AspNetCore.Http;
using SuperMagazine.Domain.Models;

namespace SuperMagazine.Services.Interfaces
{
	public interface IUserAuthorizationService
	{
		Task<bool> Register(UserRegisterViewModel userRegister);

		Task<bool> Authentication(UserLoginViewModel userLogin, HttpContext context);
	}
}
