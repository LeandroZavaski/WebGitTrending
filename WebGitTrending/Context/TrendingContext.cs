using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using static WebGitTrending.Models.TrendingModel;

namespace WebGitTrending.Context
{
    public class TrendingContext : DbContext
    {
        public TrendingContext() : base("TrendingContext")
        {
        }
        public DbSet<RootObject> RootObject { get; set; }
        public DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}