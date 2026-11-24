using meow.core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF
{
    public class CatsContext : DbContext
    {
        /// <summary>
        /// Таблица котов в бд
        /// </summary>
        public DbSet<Cat> Cats { get; set; }
        /// <summary>
        /// настраивает подключение к бд
        /// </summary>
        /// <param name="optionsBuilder">построитель параметров контекста</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseSettings.ConnectionString);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    DatabaseSettings.ConnectionString);
            }
        }
    }
}