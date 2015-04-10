using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantPoll.Models;

namespace RestaurantPoll.Controllers
{
    public class PollController : Controller
    {
        [RestaurantPoll.Filters.Authorize]
        public ActionResult Index()
        {
            return View(Poll.FindOrCreatePoll());
        }

        [RestaurantPoll.Filters.Authorize]
        [HttpPost]
        public ActionResult Vote(int restaurantId = -1)
        {
            var day = Poll.FindOrCreatePoll().GetCurrentDay();
            if (restaurantId != -1)
            {
                var restaurant = Restaurant.Restaurants.Where(r => r.Id == restaurantId).First();
                day.Vote((User)Session["user"], restaurant);
            }
            return RedirectToAction("Index");
        }

        [RestaurantPoll.Filters.Authorize]
        public ActionResult NextDay()
        {
            Poll.NextPollDay();

            return RedirectToAction("Index");
        }
    }
}