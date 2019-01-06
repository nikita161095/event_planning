﻿using event_planning.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace event_planning.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var e = 0;
            var random = new Random();

            List<Event> events = new List<Event>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach(var item in db.Events)
                {
                    e++;
                }
                var randomNumber = random.Next(1, e-1);
                ViewBag.num = randomNumber;
                ViewBag.n = 0;

                events = db.Events.ToList();
            }

            return View(events);
        }

        [Authorize(Roles = "admin")]
        public ActionResult GetUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        [Authorize(Roles = "admin")]
        public ActionResult GetMyRoles()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult About(Event event_)
        {
            if (event_.Name == null)
            {
                return View("About");
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Events.Add(event_);
                db.SaveChanges();
            }

            return RedirectToAction("About");
        }

        public ActionResult Events()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return View(db.Events);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Reg(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int e_id = id;
            bool page = false;
            foreach (var item in db.Events)
            {
                if(e_id == item.Id)
                {
                    page = true;
                    break;
                }
            }
            if(page == false)
            {
                return View("Error");
            }
            foreach (var item in db.Participants)
            {
                if (User.Identity.GetUserName() == item.User & e_id == item.EventId)
                {
                    ViewBag.f_reg = true;
                }
            }

            Event eventid = db.Events.Find(id);
            return View(eventid);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Reg(Participant participant)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach(var item in db.Participants)
                {
                    if(User.Identity.GetUserName() == item.User & participant.Name == item.Name)
                    {

                        return View("Reg_fail");
                    }
                }
                participant.User = User.Identity.GetUserName();
                participant.EventId = participant.Id;
                db.Participants.Add(participant);
                db.SaveChanges();
            }
            return View("Reg_success");
        }

        [Authorize]
        public ActionResult Reg_success()
        {
            return View();
        }

        [Authorize]
        public ActionResult Reg_fail()
        {
            return View();
        }

        [Authorize]
        public ActionResult GetMyEvents()
        {
            List<Participant> events = new List<Participant>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                events = db.Participants.ToList();
            }
            return View(events);
        }



    }
}