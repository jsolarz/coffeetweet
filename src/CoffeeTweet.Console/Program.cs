using System;
using System.Timers;
using CoffeeTweet.Domain.Entities;
using Twitterizer;

namespace CoffeeTweet.ConsoleService
{
	class Program
	{
		private static Timer _timer;

		static void Main(string[] args)
		{
			_timer = new Timer(1000);
			// Hook up the Elapsed event for the timer.
			_timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

			// Set the Interval to 2 seconds (2000 milliseconds).
			_timer.Interval = 60000;
			_timer.Enabled = true;

			Console.WriteLine("Press the Enter key to exit the program.");
			Console.ReadLine();
		}

		private static void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			Console.WriteLine("Searching tweets...");
			TwitterResponse<TwitterSearchResultCollection> searchResult = TwitterSearch.Search("#ineedcoffee");

			if (searchResult.Result == RequestResult.Success)
			{
				Console.WriteLine("Processing " + searchResult.ResponseObject.Count + " tweets");
				foreach (var tweet in searchResult.ResponseObject)
				{
					Coffee.Add(tweet);
				}
			}

			Console.WriteLine("Done! Waiting for next batch");
		}
	}
}
