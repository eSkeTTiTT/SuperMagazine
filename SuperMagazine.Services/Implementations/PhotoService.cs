using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SuperMagazine.Services.Interfaces;

namespace SuperMagazine.Services.Implementations
{
	public class PhotoService : IPhotoService
	{
		#region Properties

		private readonly Cloudinary _cloudinary;

		#endregion

		#region Constructor

		public PhotoService(IOptions<Account> config)
		{
			_cloudinary = new Cloudinary(config.Value);
		}

		#endregion

		#region Methods

		public async Task<ImageUploadResult> AddPhotoAsync(IFormFile photo)
		{
			var uploadResult = new ImageUploadResult();

			if (photo.Length > 0)
			{
				using var stream = photo.OpenReadStream();
				var uploadParams = new ImageUploadParams
				{
					File = new FileDescription(photo.FileName, stream)
				};

				uploadResult = await _cloudinary.UploadAsync(uploadParams);
			}

			return uploadResult;
		}

		public Task<DeletionResult> DeletePhotoAsync(string url)
		{
			string publicId = url.Split('/')
				.Last()
				.Split('.')
				.First();

			var deleteParams = new DeletionParams(publicId);
			var result = _cloudinary.DestroyAsync(deleteParams);

			return result;
		}

		#endregion
	}
}
