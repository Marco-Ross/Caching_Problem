using Microsoft.Extensions.Caching.Memory;
using System;

namespace Caching_
{
	public class Cache<TItem> : ICacheRegister<TItem>
	{
		private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

		public MemoryCacheEntryOptions MemoryCacheEntryOptions()
		{
			return new MemoryCacheEntryOptions()
				.SetSize(20)
				.SetPriority(CacheItemPriority.High)
				.SetSlidingExpiration(TimeSpan.FromHours(24))
				.SetAbsoluteExpiration(TimeSpan.FromDays(7));
		}

		public TItem GetOrCreate(object key, Func<TItem> createItem)
		{
			if (!_cache.TryGetValue(key, out TItem cacheEntry))
			{
				cacheEntry = createItem(); //Call db GET function if no cache data found
	
				_cache.Set(key, cacheEntry, MemoryCacheEntryOptions()); //Save data in cache.
			}
			return cacheEntry;
		}

		public TItem SetInCache(object key, object value, Func<TItem> createItem)
		{
			_cache.Remove(key); //Removing so it can update the cache

			TItem cacheEntry = createItem(); //Call db SAVE function before caching

			_cache.Set(key, value, MemoryCacheEntryOptions()); //Save data in cache.

			return cacheEntry;
		}
	}
}

/*Ideas:
 * Update cache on save.
 * Have listener that checks places where information is updated and remove key if so.
 * E.g. If attachment added to requisition, just remove cache entry for entire batch.cheque object and add it again in the background without user knowing. (background key remove/add when UPDATED)
 * When adding, just add to database and immediately to cache afterwards.
 * Background cache refresh wont be needed then?
 */

/*Issues:
 * Should all cache data be saved in one place or different instances of cache for each area.
 * How will subs be handled with cache?
 * Saving to disk?
 * Startup caching for faster loading?
*/

/* Process:
 * Caching values on startup.
 * Caching when saving/updating. (Remove existing key and replace it in cache).
 * 
 * 
 * 
 * Create task -> send to server
 * Call caching save/update (remove from cache if updating existing task).
 * Once saved in DB via sub or mapping, add to cache.
 * Next reload wont have to call from sub, can use cache.
 */