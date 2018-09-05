using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PageCheckerAPI.Models;

namespace PageCheckerAPI.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //enable lazy loading
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
    }
}
