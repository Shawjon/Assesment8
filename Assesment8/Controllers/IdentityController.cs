using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Assesment8.Models;

namespace Assesment8.Controllers
{
    public class IdentityController : Controller
    {
        //Seting up the identity
        public UserManager<IdentityUser> UserManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();

        // GET: Identity
        [Authorize]
        public ActionResult Index()
        {
                JonPartyDBEntities ORM = new JonPartyDBEntities();
                ViewBag.GuestList = ORM.Guests.ToList();
                return View();
                        
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationModel registrationGuest)
        {
            if (ModelState.IsValid)
            {

                var IdentityResult = await UserManager.CreateAsync(new IdentityUser(registrationGuest.UserName), registrationGuest.Password);

                if (IdentityResult.Succeeded)
                {

                    //if the model is valid and it passes identity, we add the user to our User table as well...

                    JonPartyDBEntities ORM = new JonPartyDBEntities();

                    //create the user based on our user model and assign the properties from the identity user to it...
                    var newGuest = new Guest();

                    newGuest.FirstName = registrationGuest.FirstName;
                    newGuest.LastName = registrationGuest.LastName;
                    newGuest.AttendanceDate = registrationGuest.AttendanceDate;
                    newGuest.EmailAddress = registrationGuest.UserName;
                    newGuest.PlusOne = registrationGuest.PlusOne;
                                        
                    //add the user to the ORM and save changes...
                    ORM.Guests.Add(newGuest);
                    ORM.SaveChanges();

                    //return home view
                    return RedirectToAction("Index", "Home");
                    
                }

                ModelState.AddModelError("", IdentityResult.Errors.FirstOrDefault());

                return View();

            }

            return View();

        }
        public ActionResult Login()
        {
            return View();
        }
            
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;

                IdentityUser user = UserManager.Find(loginModel.UserName, loginModel.Password);

                if (user != null)
                {

                    var ident = UserManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created.
                    authenticationManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Index", "Home");
                }

            }

            ModelState.AddModelError("", "Invalid login");

            return View(loginModel);
        }
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            
            if (User.Identity.Name != null)
            {

                //use the instance that has been created.
                authenticationManager.SignOut();
                
                return RedirectToAction("Index", "Home");
                
            }
            return RedirectToAction("Index", "Home");

        }


        [Authorize]
        public ActionResult Summary()
        {
            //grab the currently logged in in user's email address
            string userEmailAddress = User.Identity.Name;

            JonPartyDBEntities ORM = new JonPartyDBEntities();

            //pull the user from the ORM based on email
            Guest currentGuest = ORM.Guests.FirstOrDefault(i => i.EmailAddress == userEmailAddress);


            //pass that user to the view
            return View(currentGuest);
        }
    }
}