using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeTweet.Domain.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string TwitterUserId { get; set; }
		public string ScreenName { get; set; }
		public string TwitterAccessKey { get; set; }
		public string TwitterAccessSecret { get; set; }

		public static void Add(string userId, string screenName, string token, string tokenSecret)
		{
			using (CoffeeTweetDbContext db = new CoffeeTweetDbContext())
			{
				var user = db.Users.SingleOrDefault(c => c.TwitterUserId == userId);
				if (user == null)
				{
					user = new CoffeeTweet.Domain.Entities.User()
					{
						TwitterUserId = userId,
						ScreenName = screenName,
						TwitterAccessKey = token,
						TwitterAccessSecret = tokenSecret
					};

					db.Users.Add(user);
					db.SaveChanges();
				}
			}
		}
	}
}
