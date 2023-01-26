using Microsoft.Extensions.Caching.Memory;

namespace SuperMagazine.DAL.Interfaces
{
	public abstract class BaseCacheRepository<T>
	{
		public BaseCacheRepository()
		{
			CacheOptions = new MemoryCacheEntryOptions()
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
				Priority = 0
			};

			var callbackRegistration = new PostEvictionCallbackRegistration();
			callbackRegistration.EvictionCallback = PostEvictionCallback;
			CacheOptions.PostEvictionCallbacks.Add(callbackRegistration);
		}

		protected abstract void AddCache(T entity);

		protected virtual MemoryCacheEntryOptions CacheOptions { get; set; } = null!;

		protected virtual void PostEvictionCallback(object key, object? value, EvictionReason reason, object? state)
		{
			Console.WriteLine("Запись из кэша удалена");
		}
	}
}
