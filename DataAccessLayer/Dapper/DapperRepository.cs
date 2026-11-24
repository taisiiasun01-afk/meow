using Dapper;
using meow.core.interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using meow.core.Models;

namespace DataAccessLayer.Dapper
{
    public class CatDapperRepository : IRepository<Cat>
    {
        /// <summary>
        /// строка подключения к бд
        /// </summary>
        private readonly string connectionString;
        /// <summary>
        /// создает репозиторий с настройками подключения по умолчанию
        /// </summary>
        public CatDapperRepository()
        {
            this.connectionString = DatabaseSettings.ConnectionString;
        }
        /// <summary>
        /// создать нового кода в бд
        /// </summary>
        /// <param name="obj">кот которого нужно добавить</param>
        public void Create(Cat obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Cats (Name, Breed, Age, Weight, Color) " +
                          "VALUES (@Name, @Breed, @Age, @Weight, @Color)";
                connection.Execute(sql, obj);
            }
        }
        /// <summary>
        /// получить всех котов из бд
        /// </summary>
        /// <returns>список котов</returns>
        public IEnumerable<Cat> ReadAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Cat>("SELECT * FROM Cats").ToList();
            }
        }
        /// <summary>
        /// получить кота по айди
        /// </summary>
        /// <param name="id">номер кота</param>
        /// <returns>аота</returns>
        public Cat ReadById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<Cat>(
                    "SELECT * FROM Cats WHERE Id = @Id", new { Id = id });
            }
        }
        /// <summary>
        /// обновить кота в бд
        /// </summary>
        /// <param name="obj">кот</param>
        public void Update(Cat obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "UPDATE Cats SET Name = @Name, Breed = @Breed, " +
                          "Age = @Age, Weight = @Weight, Color = @Color WHERE Id = @Id";
                connection.Execute(sql, obj);
            }
        }
        /// <summary>
        /// удалить кота из бд
        /// </summary>
        /// <param name="obj">кот</param>
        public void Delete(Cat obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM Cats WHERE Id = @Id", new { Id = obj.Id });
            }
        }
    }
}