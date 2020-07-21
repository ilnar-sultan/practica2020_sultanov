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
                model.Photo = ImageSaveHelper.SaveImage(imageData);
            }

            // автор - пользователь с текущим Id
            var userId = Convert.ToInt32(Session["UserId"]);
            var userInDb = dbContext.Users.FirstOrDefault(c => c.Id == userId);

            model.Author = userInDb;
            
            dbContext.Posts.Add(model);

            dbContext.SaveChanges();

            var posts = dbContext.Posts.OrderByDescending(c => c.Id).ToList();
            ViewBag.Posts = posts;
            return View("Index");
        }

    }
}