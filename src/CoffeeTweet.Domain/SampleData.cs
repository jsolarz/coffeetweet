using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace CoffeeTweet.Domain
{
	public class SampleData : DropCreateDatabaseIfModelChanges<CoffeeTweetDbContext>
	{
		protected override void Seed(CoffeeTweetDbContext context)
		{
		}
	}
}
