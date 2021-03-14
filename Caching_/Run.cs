using System;

namespace Caching_
{
	class Run
	{
		static void Main(string[] args)
		{
			var users = new UserImpl();
			string userInput = "";

			while (userInput != "exit")
				AddUserToDict(users);
		}

		private static void AddUserToDict(IUser users)
		{
			string userInput = Console.ReadLine();
			var values = userInput.Split(',');

			var key = values[0];
			var value =	values[1];
			
			if (value.Equals("get"))
			{
				var username = users.Get(key);

				if (!string.IsNullOrWhiteSpace(username))
					Console.WriteLine(username);
			}
			else
				users.Save(key, value);	
		}
	}
}
