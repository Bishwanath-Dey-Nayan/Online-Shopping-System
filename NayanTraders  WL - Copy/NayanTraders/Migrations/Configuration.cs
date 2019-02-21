namespace NayanTraders.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NayanTraders.Models.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NayanTraders.Models.DataBaseContext";
        }

        protected override void Seed(NayanTraders.Models.DataBaseContext context)
        {
            Models.DataBaseContext db = new Models.DataBaseContext();
            db.Countries.Add(new Models.Country() { Id = 1, Name = "Bangladesh" });
            db.SaveChanges();
            db.Cities.Add(new Models.City() { Id = 1, Name = "Bangladesh" });
            db.SaveChanges();
        }
    }
}
