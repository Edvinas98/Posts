using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Core.Models;

namespace Posts.Core.Contracts
{
    public interface IPostRepository
    {
        List<Post> GetPosts(List<User> users);
        void AddPost(Post post);
        void DeletePost(Post post);
    }
}
