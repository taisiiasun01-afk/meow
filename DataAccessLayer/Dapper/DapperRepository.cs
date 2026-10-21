using Dapper;
using meow.core.interfaces;
using meow.core.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer.Dapper
{
    /// <summary>
    /// Репозиторий на основе Dapper
    /// </summary>
    public class CatDapperRepository : IRepository<Cat>
    {
        /// <summary>Строка подключения к базе данных.</summary>
        private readonly string connectionString;

        /// <summary>
        /// Создаёт репозиторий с настройками подключения по умолчанию.
        /// </summary>
        public CatDapperRepository()
        {
            this.connectionString = DatabaseSettings.ConnectionString;
        }

        /// <summary>
        /// Создать нового кота в базе данных.
        /// Выполняет SQL-запрос INSERT.
        /// </summary>
        /// <param name="obj">Объект Cat, который нужно добавить.</param>
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
        /// Получить всех котов из базы данных.
        /// Выполняет SQL-запрос SELECT.
        /// </summary>
        /// <returns>Список объектов Cat. Если котов нет — пустой список.</returns>
        public IEnumerable<Cat> ReadAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Cat>("SELECT * FROM Cats").ToList();
            }
        }

        /// <summary>
        /// Получить кота по Id.
        /// </summary>
        /// <param name="id">Идентификатор кота.</param>
        /// <returns>Объект Cat или null, если не найден.</returns>
        public Cat ReadById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<Cat>(
                    "SELECT * FROM Cats WHERE Id = @Id", new { Id = id });
            }
        }

        /// <summary>
        /// Обновить существующего кота в базе данных.
        /// Выполняет SQL-запрос UPDATE.
        /// </summary>
        /// <param name="obj">Объект Cat с обновлёнными данными. Поле Id — ключ.</param>
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
        /// Удалить кота из базы данных.
        /// Выполняет SQL-запрос DELETE.
        /// </summary>
        /// <param name="obj">Объект Cat, который нужно удалить. Поле Id — ключ.</param>
        public void Delete(Cat obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM Cats WHERE Id = @Id", new { Id = obj.Id });
            }
        }
    }
}