using Assesment8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assesment8.Controllers
{
    public class DatabaseController : Controller
    {
       
        // GET: Database
        
        public ActionResult Index()
        {
            JonPartyDBEntities ORM = new JonPartyDBEntities();
            ViewBag.GuestList = ORM.Guests.ToList();
            return View();

        }
        [Authorize]
        public ActionResult AddGuest()
        {
            return View();
        }
        public ActionResult SaveGuest(Guest newGuest)
        {
            JonPartyDBEntities ORM = new JonPartyDBEntities();

            if (newGuest != null)
            {
                ORM.Guests.Add(newGuest);
                ORM.SaveChanges();
            }


            return RedirectToAction("Summary", newGuest);
        }
        public ActionResult Summary(Guest guest)
        {

            return View(guest);
        }
        public ActionResult EditGuest(int GuestID)
        {
            JonPartyDBEntities ORM = new JonPartyDBEntities();
            Guest found = ORM.Guests.Find(GuestID);

            return View(found);
        }
        public ActionResult SaveGuestChanges(Guest updatedGuest)
        {
            JonPartyDBEntities ORM = new JonPartyDBEntities();
            Guest oldGuest = ORM.Guests.Find(updatedGuest.GuestID);
            oldGuest.FirstName = updatedGuest.FirstName;
            oldGuest.LastName = updatedGuest.LastName;
            oldGuest.AttendanceDate = updatedGuest.AttendanceDate;
            oldGuest.EmailAddress = updatedGuest.EmailAddress;
            oldGuest.PlusOne = updatedGuest.PlusOne;


            ORM.Entry(oldGuest).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteGuest(int GuestID)
        {
            JonPartyDBEntities ORM = new JonPartyDBEntities();
            Guest found = ORM.Guests.Find(GuestID);

            ORM.Guests.Remove(found);
            ORM.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult DishList()
        {
            string currentemail = User.Identity.Name;
            JonPartyDBEntities ORM = new JonPartyDBEntities();
            ViewBag.DishList = ORM.Dishes.Where(x => x.EmailAddress == currentemail).ToList();
            return View();

        }
        [Authorize]
        public ActionResult AddDish()
        {
            return View();
        }
        [Authorize]
        public ActionResult SaveDish(Dish newDish)
        {
            JonPartyDBEntities ORM = new JonPartyDBEntities();

            if (newDish != null)
            {
                ORM.Dishes.Add(newDish);
                ORM.SaveChanges();
            }


            return RedirectToAction("DishSummary", newDish);
        }







        public ActionResult DishSummary(Dish dish)
        {

            return View(dish);
        }
        public ActionResult EditDish(int DishID)
        {
            JonPartyDBEntities ORM = new JonPartyDBEntities();
            Dish found = ORM.Dishes.Find(DishID);

            return View(found);
        }
        public ActionResult SaveDishChanges(Dish updatedDish)
        {
            JonPartyDBEntities ORM = new JonPartyDBEntities();
            Dish oldDish = ORM.Dishes.Find(updatedDish.DishID);
            oldDish.PersonName = updatedDish.PersonName;
            oldDish.PhoneNumber = updatedDish.PhoneNumber;
            oldDish.DishName = updatedDish.DishName;
            oldDish.DishDescription = updatedDish.DishDescription;
            oldDish.Options = updatedDish.Options;



            ORM.Entry(oldDish).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();
            return RedirectToAction("DishList");
        }
        [Authorize]
        public ActionResult DeleteDish(int DishID)
        {
            JonPartyDBEntities ORM = new JonPartyDBEntities();
            Dish found = ORM.Dishes.Find(DishID);

            ORM.Dishes.Remove(found);
            ORM.SaveChanges();

            return RedirectToAction("DishList");
        }
    }
}
