using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using meow.core.interfaces;

namespace DataAccessLayer.Dapper
{
    /// <summary>
    /// Репозиторий на основе Dapper
    /// </summary>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        private readonly string connectionString;

        private readonly string tableName = typeof(T).Name+ "s";

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
                var properties = typeof(T).GetProperties()
                    .Where(p => p.Name != "Id")
                    .Select(p => p.Name);

                var columns = string.Join(", ", properties);
                var values = string.Join(", ", properties.Select(p => "@" + p));

                var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                connection.Execute(sql, obj);
            }
        }

        /// <summary>
        /// Получить все записи.
        /// </summary>
        public IEnumerable<T> ReadAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>($"SELECT * FROM {tableName}").ToList();
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
                    $"SELECT * FROM {tableName} WHERE Id = @Id", new { Id = id });
            }
        }

        /// <summary>
        /// Обновить существующую запись.
        /// </summary>
        public void Update(T obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var properties = typeof(T).GetProperties()
                    .Where(p => p.Name != "Id")
                    .Select(p => $"{p.Name} = @{p.Name}");

                var setClause = string.Join(", ", properties);

                var sql = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";
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
                connection.Execute($"DELETE FROM {tableName} WHERE Id = @Id", new { Id = obj.Id });
            }
        }
    }
}