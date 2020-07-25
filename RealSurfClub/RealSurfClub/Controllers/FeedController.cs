using RealSurfClub.Controllers.DAL;
using RealSurfClub.Helpers;
using RealSurfClub.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealSurfClub.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        private SurfDbContext dbContext = new SurfDbContext();

        public ActionResult Index()
        {
            var posts = dbContext.Posts.OrderByDescending(c => c.Id).ToList();
            ViewBag.Posts = posts;
            return View();
        }

        [HttpPost]
        public ActionResult AddPost(Post model, HttpPostedFileBase imageData)
        {

            if (!ModelState.IsValid)
            {
                var postsDb = dbContext.Posts.OrderByDescending(c => c.Id).ToList();
                ViewBag.Posts = postsDb;

                return View("Index", model);
            }

            if (imageData == null && model.Text == null)
            {
                ModelState.AddModelError(string.Empty, "Не загружено изображение или отсутствует текст");

                var postsDb = dbContext.Posts.OrderByDescending(c => c.Id).ToList();
                ViewBag.Posts = postsDb;

                return View("Index", model);

            }

            model.PublishDate = DateTime.Now;

            //model.Photo = Guid.NewGuid(); // надо обрабатывать и прикреплять изобр-ия

            if (imageData != null)
            {
                if (!ImageFormatHelper.IsJpg(imageData))
                {

                    ModelState.AddModelError(string.Empty, "Загруженное изображение не картинка формата JPG");
                    var postsdb = dbContext.Posts.OrderByDescending(c => c.Id).ToList();
                    ViewBag.Posts = postsdb;
                    return View("Index", model);
                }

                model.Photo = ImageSaveHelper.SaveImage(imageData);
            }

            // автор - пользователь с текущим Id
            var userId = Convert.ToInt32(Session["UserId"]);
            var userInDb = dbContext.Users.FirstOrDefault(c => c.Id == userId);

            if(userInDb == null)
            {
                // пользователь не автроизован
                TempData["errormessage"] = "Время сессии истекло. Авторизуйтесь повторно.";
                return RedirectToAction("Index", "Home");
            }

            model.Author = userInDb;
            
            dbContext.Posts.Add(model);

            dbContext.SaveChanges();

            var posts = dbContext.Posts.OrderByDescending(c => c.Id).ToList();
            ViewBag.Posts = posts;

            ModelState.Clear();

            return View("Index");
        }

    }
}