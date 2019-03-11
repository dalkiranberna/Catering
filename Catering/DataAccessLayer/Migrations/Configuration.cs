namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.CateringContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccessLayer.CateringContext context)
        {
            /*if (!context.Users.Any(x => x.Email == "adminc@c.com"))
		  {
			  IdentityRole role = new IdentityRole();
			  role.Name = "Chef";
			  context.Roles.Add(role);
			  context.SaveChanges();

			  UserStore<Member> chefStore = new UserStore<Member>(context);
			  UserManager<Member> chefManager = new UserManager<Member>(chefStore);

			  Member chef = context.Users.Where(x => x.Email == "adminc@c.com").FirstOrDefault();
			  chefManager.AddToRole(chef.Id, "Chef");

			  context.SaveChanges();
		  }*/
        }
    }
}
