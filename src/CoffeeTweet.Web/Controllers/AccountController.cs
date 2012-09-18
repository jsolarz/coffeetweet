using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Twitterizer;
using System.Web.Security;
using CoffeeTweet.Domain;
using CoffeeTweet.Web.Models;

namespace CoffeeTweet.Controllers
{
	public class AccountController : Controller
	{
		public ActionResult LogOn()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LogOn(LogOnModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(model.UserName, model.Password))
				{
					FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
					if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
						&& !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult TwitterLogon(string oauth_token, string oauth_verifier, string ReturnUrl)
		{
			if (string.IsNullOrEmpty(oauth_token) || string.IsNullOrEmpty(oauth_verifier))
			{
				UriBuilder builder = new UriBuilder(this.Request.Url);
				builder.Query = string.Concat(
					builder.Query,
					string.IsNullOrEmpty(builder.Query) ? string.Empty : "&",
					"ReturnUrl=", // + ConfigurationManager.AppSettings["baseUrl"]
					ReturnUrl);

				string token = OAuthUtility.GetRequestToken(
					ConfigurationManager.AppSettings["TwitterConsumerKey"],
					ConfigurationManager.AppSettings["TwitterConsumerSecret"],
					builder.ToString()).Token;

				return Redirect(OAuthUtility.BuildAuthorizationUri(token, true).ToString());
			}

			var tokens = OAuthUtility.GetAccessToken(
				ConfigurationManager.AppSettings["TwitterConsumerKey"],
				ConfigurationManager.AppSettings["TwitterConsumerSecret"],
				oauth_token,
				oauth_verifier);

			CoffeeTweet.Domain.Entities.User.Add(tokens.UserId.ToString(), tokens.ScreenName, tokens.Token, tokens.TokenSecret);
			FormsAuthentication.SetAuthCookie(tokens.ScreenName, false);

			if (string.IsNullOrEmpty(ReturnUrl))
				return Redirect("/");
			else
				return Redirect(ReturnUrl);
		}

	}
}
