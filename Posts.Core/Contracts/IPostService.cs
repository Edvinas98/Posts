using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Core.Models;
using Posts.Core.Repositories;
using Posts.Core.Services;

namespace Posts.Core.Contracts
{
    public interface IPostService
    {
        void ReadPosts();
        string AddPost(Post post);
        string DeletePost(Post post);
        List<Post> GetPosts();
        public List<User> GetUsers();
        string AddUser(User user);
        string DeleteUser(User user);
    }
}
