namespace Caching_
{
	public class UserImpl : IUser
	{
		private static readonly UserInMemDatabase<string, string> _database = UserInMemDatabase<string, string>.Instance;
		//private static readonly ICacheRegister<string> _cacheRegister = new Cache<string>();
		private static readonly Cache<string> _cache = new Cache<string>();

		public string Get(string key) => _cache.GetOrCreate(key, () => _database.GetData(key));
		public string Save(string key, string value) => _cache.SetInCache(key, value, () => _database.SaveData(key, value));
	}
}
