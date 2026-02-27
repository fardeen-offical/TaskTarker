using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskTracker.Models;
using TaskTracker.Filters; // Agar aapne ise yahan use karna hai

namespace TasKTarker.Controllers
{
    public class AccountController : Controller
    {
        // 1. DATABASE CONTEXT ADD KAREIN
        private TaskDbContext db = new TaskDbContext();

        [HttpGet]
        public ActionResult Login() { return View(); }

        [HttpPost]
        
        public ActionResult Login(string username, string password)
        {
            // 1. Database se user dhundo jo input se match kare
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Agar mil gaya toh Token generate karo
                var token = JwtManager.GenerateToken(user.Username);

                HttpCookie authCookie = new HttpCookie("jwt", token);
                authCookie.HttpOnly = true;
                Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Task");
            }

            // 2. Agar nahi mila toh error dikhao
            ViewBag.Error = "Ghalat Username ya Password! Pehle Register karein.";
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var userExists = db.Users.Any(u => u.Username == user.Username);
                if (userExists)
                {
                    ViewBag.Error = "Ye Username pehle se maujood hai!";
                    return View();
                }

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(user);
        }

        // 3. LOGOUT BHI ZAROORI HAI
        public ActionResult Logout()
        {
            HttpCookie cookie = new HttpCookie("jwt");
            cookie.Expires = DateTime.Now.AddDays(-1); // Expire kar dein
            Response.Cookies.Add(cookie);
            return RedirectToAction("Login");
        }
    }
}