using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeTweet.Domain;

namespace CoffeeTweet.Web.Controllers
{
	public class BaseController : Controller
	{
		protected CoffeeTweetDbContext _db;

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			_db = new CoffeeTweetDbContext();
			base.OnActionExecuting(filterContext);
		}

		protected override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			_db.Dispose();
			base.OnResultExecuted(filterContext);
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			_db.SaveChanges();
			base.OnActionExecuted(filterContext);
		}
	}
}
