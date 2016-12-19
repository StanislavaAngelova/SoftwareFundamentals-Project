﻿using EventSpot.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EventSpot.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            ViewBag.Message = "Main page";

            return RedirectToAction("List", "Event");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult ListCategories()
        {
            using (var database = new EventSpotDbContext())
            {
                var categories = database.Categories
                    .Include(c => c.Events)
                    .OrderBy(c => c.Name)
                    .ToList();

                return View(categories);

            }
        }

        public ActionResult ListEvents(int? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var database = new EventSpotDbContext())
            {
                var events = database.Events
                    .Where(a => a.CategoryId == categoryId)
                    .Include(a => a.Organizer)
                    .ToList();

                return View(events);

            }
           
        }

    }
}