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
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly string connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CatsDatabase.mdf;Integrated Security=True";

        public void Add(T entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Cats (Name, Breed, Age, Weight, Color) " +
                          "VALUES (@Name, @Breed, @Age, @Weight, @Color)";
                connection.Execute(sql, entity);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "DELETE FROM Cats WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }

        public IEnumerable<T> ReadAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>("SELECT * FROM Cats").ToList();
            }
        }

        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM Cats WHERE Id = @Id";
                return connection.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }

        public void Update(T entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "UPDATE Cats SET Name = @Name, Breed = @Breed, " +
                          "Age = @Age, Weight = @Weight, Color = @Color WHERE Id = @Id";
                connection.Execute(sql, entity);
            }
        }
    }
}
