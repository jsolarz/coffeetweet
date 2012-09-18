using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeTweet.Domain.Entities;

namespace CoffeeTweet.Web.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		//
		// GET: /Home/
		public ActionResult Index()
		{
			ViewBag.Coffees = _db.Coffees.ToList();
			ViewBag.Coffees = ViewBag.Coffees ?? new List<Coffee>();

			var count = _db.Coffees.GroupBy(c => c.User)
				.Select(g => new { userId = g.Key, count = g.Count() })
				.OrderByDescending(x => x.count);

			Dictionary<User, int> mostDrinker = new Dictionary<User, int>();

			foreach (var item in count)
			{
				var user = _db.Users.Where(c => c.TwitterUserId == item.userId).SingleOrDefault();
				if (user != null)
					mostDrinker.Add(user, item.count);
			}

			ViewBag.MostDrinkers = mostDrinker;

			return View();
		}
	}
}
