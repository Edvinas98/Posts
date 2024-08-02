using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Core.Contracts;
using Posts.Core.Repositories;
using Posts.Core.Services;
using Posts.Services;

namespace Posts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dataBaseString = "Server=localhost;Database=posts;Trusted_Connection=True;";

            IUserRepository userRepository = new UserRepository(dataBaseString);
            IPostRepository postRepository = new PostRepository(dataBaseString);
            IUserService userService = new UserService(userRepository);
            IPostService postService = new PostService(postRepository, userService);
            MenuUI menu = new MenuUI(postService);
            menu.LaunchMenu();
        }
    }
}
