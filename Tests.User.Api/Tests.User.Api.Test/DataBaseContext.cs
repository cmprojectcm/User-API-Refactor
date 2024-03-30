using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tests.User.Api.Test
{
    public class DataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Must stay as in memory database
            optionsBuilder.UseInMemoryDatabase("Tests.User.Api.Tests");
        }
        

        public DbSet<Models.User> Users { get; set; }
    }
}