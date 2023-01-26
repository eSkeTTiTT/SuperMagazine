using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace SuperMagazine.Services.Interfaces
{
	public interface IPhotoService
	{
		Task<ImageUploadResult> AddPhotoAsync(IFormFile photo);

		Task<DeletionResult> DeletePhotoAsync(string url);
	}
}
