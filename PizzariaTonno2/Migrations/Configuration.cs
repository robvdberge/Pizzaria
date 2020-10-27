namespace PizzariaTonno2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PizzariaTonno2.Models.PizzariaTonno2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PizzariaTonno2.Models.PizzariaTonno2Context";
        }

        protected override void Seed(PizzariaTonno2.Models.PizzariaTonno2Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
