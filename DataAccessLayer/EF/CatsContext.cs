using meow.core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF
{
    /// <summary>
    /// Контекст базы данных для Entity Framework.
    /// </summary>
    public class CatsContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
        @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=CatsDatabase;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}