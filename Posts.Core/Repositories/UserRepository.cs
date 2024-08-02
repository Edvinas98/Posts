using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Posts.Core.Contracts;
using Posts.Core.Models;

namespace Posts.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _dbConnectionString;

        public UserRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
        public List<User> GetUsers()
        {
            List<User> tempList = new List<User>();

            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                tempList = dbConnection.
                Query<User>("SELECT name, email FROM users ORDER BY name").ToList();
            }
            return tempList;
        }

        public void AddUser(User user)
        {
            string sqlCommand = "INSERT INTO users (name, email) VALUES (@Name, @Email)";

            var parameters = new
            {
                Name = user.Name,
                Email = user.Email,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        public void DeleteUser(User user)
        {
            CheckForPosts(user);

            string sqlCommand = @"DELETE FROM users WHERE name = @Name AND email = @Email";

            var parameters = new
            {
                Name = user.Name,
                Email = user.Email,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        private void CheckForPosts(User user)
        {
            string sqlCommand = "SELECT id FROM users WHERE name = @Name AND email = @Email";

            int userId;

            var parameters1 = new
            {
                Name = user.Name,
                Email = user.Email
            };

            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                userId = dbConnection.
                QueryFirst<int>(sqlCommand, parameters1);
            }

            sqlCommand = @"DELETE FROM posts WHERE id_user = @UserID";

            var parameters = new
            {
                UserID = userId,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }
    }
}
