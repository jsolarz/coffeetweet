using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeTweet.Domain.Entities
{
	public class Coffee
	{
		public int Id { get; set; }
		public string TweetId { get; set; }
		public string User { get; set; }
		public DateTime CreatedDate { get; set; }
		public string Text { get; set; }

		public static void Add(Twitterizer.TwitterSearchResult tweet)
		{
			using (CoffeeTweetDbContext db = new CoffeeTweetDbContext())
			{
				var user = db.Users.SingleOrDefault(c => c.TwitterUserId == tweet.FromUserId.ToString());

				if (user != null)
				{
					Coffee coffee = new Coffee
					{
						TweetId = tweet.Id.ToString(),
						User = tweet.FromUserId.ToString(),
						Text = tweet.Text,
						CreatedDate = tweet.CreatedDate
					};

					db.Coffees.Add(coffee);
				}
				db.SaveChanges();
			}
		}
	}
}
