using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace RestaurantPoll.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            Session["user"] = null;

            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = RestaurantPoll.Models.User.Authenticate(email, password);

            if (user != null)
            {
                Session["user"] = user;

                return RedirectToAction("Index", "Poll");
            }

            ViewBag.Message = "Usuário ou senha incorretos";

            return View();
        }
    }
}