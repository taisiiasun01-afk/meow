using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using meow.core.Interfaces;

namespace DataAccessLayer.Dapper
{
    /// <summary>
    /// Репозиторий на основе Dapper (микро-ORM, работает через SQL-запросы).
    /// </summary>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        private readonly string connectionString;

        public DapperRepository()
        {
            this.connectionString = DatabaseSettings.ConnectionString;
        }
        /// <summary>
        /// Создать новую запись.
        /// </summary>
        public void Create(T obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Cats (Name, Breed, Age, Weight, Color) " +
                          "VALUES (@Name, @Breed, @Age, @Weight, @Color)";
                connection.Execute(sql, obj);
            }
        }
        /// <summary>
        /// Получить все записи из таблицы Cats.
        /// </summary>
        public IEnumerable<T> ReadAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>("SELECT * FROM Cats").ToList();
            }
        }
        /// <summary>
        /// Получить запись по Id.
        /// </summary>
        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<T>(
                    "SELECT * FROM Cats WHERE Id = @Id", new { Id = id });
            }
        }
        /// <summary>
        /// Обновить существующую запись.
        /// </summary>
        public void Update(T obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "UPDATE Cats SET Name = @Name, Breed = @Breed, " +
                          "Age = @Age, Weight = @Weight, Color = @Color WHERE Id = @Id";
                connection.Execute(sql, obj);
            }
        }
        /// <summary>
        /// Удалить запись.
        /// </summary>
        public void Delete(T obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM Cats WHERE Id = @Id", new { Id = obj.Id });
            }
        }
    }
}
