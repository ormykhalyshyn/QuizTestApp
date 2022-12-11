using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quiz.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Tests> Tests { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Choices> Choices { get; set; }

    }
}
