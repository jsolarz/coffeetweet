using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoffeeTweet.Domain.Entities;
using System.Data.Entity;

namespace CoffeeTweet.Domain
{
	public class CoffeeTweetDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Coffee> Coffees { get; set; }
	}
}
