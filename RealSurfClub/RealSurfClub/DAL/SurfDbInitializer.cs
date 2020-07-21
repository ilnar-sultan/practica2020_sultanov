using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RealSurfClub.Models.DBModel;

namespace RealSurfClub.Controllers.DAL
{
    public class SurfDbInitializer: DropCreateDatabaseIfModelChanges<SurfDbContext>
        //DropCreateDatabaseAlways<SurfDbContext>
    {
        public object Author { get; set; }

        protected override void Seed(SurfDbContext context)
        {
            var user = new User
            {
                Nickname = "Stanlox",
                Password = "123456",
                Lastname = "Рубцов",
                Name = "Павел",
                Email = "vsel@star.ru",
                About = "ТОП-1"
            };

            var post1 = new Post
            {
                Text = "Первый тестовый пост",
                PublishDate = DateTime.Now,
                Author = user
            };

            var post2 = new Post
            {
                Text = "Второй тестовый пост",
                PublishDate = DateTime.Now,
                Author = user
            };

            context.Users.Add(user);
            context.Posts.Add(post1);
            context.Posts.Add(post2);

            context.SaveChanges();
        }
    }
}