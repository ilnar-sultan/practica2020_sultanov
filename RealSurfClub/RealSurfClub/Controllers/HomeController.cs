using RealSurfClub.Controllers.DAL;
using RealSurfClub.Helpers;
using RealSurfClub.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RealSurfClub.Controllers
{
    public class HomeController : Controller
    {

        SurfDbContext dbContext = new SurfDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault
                    (c => c.Nickname == model.Nickname &&
                     c.Password == model.Password
                );

                if(userInDb != null)
                {
                    FormsAuthentication.SetAuthCookie(userInDb.Nickname, model.RememberMe);
                    Session["userId"] = userInDb.Id.ToString();
                    Session["Nickname"] = userInDb.Nickname.ToString();
                    Session["Photo"] = ImageUrlHelper.GetUrl(userInDb.Photo);

                    return RedirectToAction("Index", "Feed");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверный псевдоним или пароль!");
                } 
            }
            return View("Index", model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Request.Cookies.Clear();
            return RedirectToAction("Index", "Feed");
        }
    }
}