using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            RestaurantPoll.Models.User user = 
                RestaurantPoll.Models.User.GetAllUsers().Find(u => u.Email == email && u.Password == password);

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