using Dapper;
using meow.core.interfaces;
using meow.core.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer.Dapper
{
    public class CatDapperRepository : IRepository<Cat>
    {
        private readonly string connectionString;

        public CatDapperRepository()
        {
            this.connectionString = DatabaseSettings.ConnectionString;
        }
        public void Create(Cat obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Cats (Name, Breed, Age, Weight, Color) " +
                          "VALUES (@Name, @Breed, @Age, @Weight, @Color)";
                connection.Execute(sql, obj);
            }
        }

        public IEnumerable<Cat> ReadAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Cat>("SELECT * FROM Cats").ToList();
            }
        }

        public Cat ReadById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<Cat>(
                    "SELECT * FROM Cats WHERE Id = @Id", new { Id = id });
            }
        }

        public void Update(Cat obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "UPDATE Cats SET Name = @Name, Breed = @Breed, " +
                          "Age = @Age, Weight = @Weight, Color = @Color WHERE Id = @Id";
                connection.Execute(sql, obj);
            }
        }

        public void Delete(Cat obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM Cats WHERE Id = @Id", new { Id = obj.Id });
            }
        }
    }
}