using System.Collections.Generic;
using System.Threading;

namespace Caching_
{
	public class UserInMemDatabase<Key, Value>
	{
		private IDictionary<Key, Value> _storedNames = new Dictionary<Key, Value>();
		private int _sleep = 2000;

		private static readonly object padlock = new object();
		private static UserInMemDatabase<Key, Value> instance = null;

		public static UserInMemDatabase<Key, Value> Instance
		{
			get
			{
				lock (padlock)
				{
					if (instance == null)
						instance = new UserInMemDatabase<Key, Value>();

					return instance;
				}
			}
		}

		public Value GetData(Key key)
		{
			Thread.Sleep(_sleep);
			return _storedNames[key];
		}

		public Value SaveData(Key key, Value value)
		{
			if (!_storedNames.ContainsKey(key))
				_storedNames.Add(key, value);

			else
				_storedNames[key] = value;

			return value;
		}
	}
}
