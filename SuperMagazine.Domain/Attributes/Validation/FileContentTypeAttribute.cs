using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SuperMagazine.Domain.Attributes.Validation
{
	public class FileContentTypeAttribute : ValidationAttribute
	{
		private readonly IEnumerable<string> _contentTypes;

		public FileContentTypeAttribute(string contentTypes)
		{
			_contentTypes = contentTypes
				.Split(',')
				.Select(i => i.Trim());
		}

		public override bool IsValid(object? value) =>
			_contentTypes.Contains((value as IFormFile)?.ContentType);
		
	}
}
