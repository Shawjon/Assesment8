using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(Assesment8.Startup))]

namespace Assesment8
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //sets up dependenceys when the app starts up---------



            //connection string
            const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JonPartyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //add connection string to db context
            app.CreatePerOwinContext(() => new IdentityDbContext(connectionString));


            //add to the owin context User Store the identity user dependency using the db context
            app.CreatePerOwinContext<UserStore<IdentityUser>>((opt, cont) => new UserStore<IdentityUser>(cont.Get<IdentityDbContext>()));


            //add to the owin context UserManager the identity user dependency using the user store
            app.CreatePerOwinContext<UserManager<IdentityUser>>(
                (opt, cont) => new UserManager<IdentityUser>(cont.Get<UserStore<IdentityUser>>()));
        }
    }
}
