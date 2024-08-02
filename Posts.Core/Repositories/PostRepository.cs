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
    struct PostInfo
    {
        public int UserID;
        public string Title;
        public string Content;
    };

    public class PostRepository : IPostRepository
    {
        private readonly string _dbConnectionString;

        public PostRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
        public List<Post> GetPosts(List<User> users)
        {
            List<PostInfo> postsInfo = new List<PostInfo>();

            using IDbConnection dbConnection1 = new SqlConnection(_dbConnectionString);
            {
                postsInfo = dbConnection1.
                Query<PostInfo>(@"SELECT id_user AS 'UserID', title, content FROM posts").ToList();
            }
            User tempUser;
            List<Post> posts = new List<Post>();
            foreach (PostInfo post in postsInfo)
            {
                tempUser = new User();

                var parameters = new
                {
                    UIserID = post.UserID
                };

                using IDbConnection dbConnection2 = new SqlConnection(_dbConnectionString);
                {
                    tempUser = dbConnection2.
                    QueryFirst<User>("SELECT name, email FROM users WHERE id = @UIserID", parameters);
                }

                if (tempUser.Name != "")
                {
                    foreach (User realUser in users)
                    {
                        if (tempUser.Name.ToLower() == realUser.Name.ToLower() && tempUser.Email == realUser.Email)
                        {
                            posts.Add(new Post(realUser, post.Title, post.Content));
                            break;
                        }
                    }
                }
            }
            return posts;
        }

        public void AddPost(Post post)
        {
            string sqlCommand = "SELECT id FROM users WHERE name = @Name AND email = @Email";

            int userId;

            var parameters1 = new
            {
                Name = post.TheUser.Name,
                Email = post.TheUser.Email
            };

            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                userId = dbConnection.
                QueryFirst<int>(sqlCommand, parameters1);
            }

            sqlCommand = "INSERT INTO posts (id_user, title, content) VALUES (@UserID, @Title, @Content)";

            var parameters = new
            {
                UserID = userId,
                Title = post.Title,
                Content = post.Content
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        public void DeletePost(Post post)
        {
            string sqlCommand = "SELECT id FROM users WHERE name = @Name AND email = @Email";

            int userId;

            var parameters1 = new
            {
                Name = post.TheUser.Name,
                Email = post.TheUser.Email
            };

            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            {
                userId = dbConnection.
                QueryFirst<int>(sqlCommand, parameters1);
            }

            sqlCommand = @"DELETE FROM posts WHERE id_user = @UserID AND title = @Title AND content = @Content";

            var parameters = new
            {
                UserID = userId,
                Title = post.Title,
                Content = post.Content
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }
    }
}
