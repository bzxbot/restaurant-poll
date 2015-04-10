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
            if (!Poll.IsValidDay(DateTime.Now))
            {
                return View("Dia de almoçar em casa!");
            }

            return View(Poll.FindOrCreatePoll());
        }

        [RestaurantPoll.Filters.Authorize]
        [HttpPost]
        public ActionResult Vote(int restaurantId = -1)
        {
            var day = Poll.FindOrCreatePoll().GetCurrentDay();
            var user = (User)Session["user"];
            if (!day.HasUserVoted(user) && restaurantId != -1)
            {
                Restaurant restaurant = Restaurant.Restaurants.Where(r => r.Id == restaurantId).First();
                day.Vote(user, restaurant);
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