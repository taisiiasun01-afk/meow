using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DatabaseSettings
    {
        /// <summary>
        /// строка подключения к бд
        /// </summary>
        public const string ConnectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=CatsDatabase;Integrated Security=True;TrustServerCertificate=True";
    }
}
 