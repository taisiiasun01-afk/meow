using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Настройки подключения к базе данных.
    /// </summary>
    public static class DatabaseSettings
    {
        public const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Пользователь\Desktop\сфу\архитектура\meow\DataAccessLayer\CatsDatabase.mdf;Integrated Security = True";
    }
}
