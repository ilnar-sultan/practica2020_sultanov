using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RealSurfClub.Models.DBModel;

namespace RealSurfClub.Controllers.DAL
{
    public class SurfDbContext : DbContext
    {
        static SurfDbContext()
        {
            Database.SetInitializer(new SurfDbInitializer());
        }

        public SurfDbContext() : base("RealSurfDatabase")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}




