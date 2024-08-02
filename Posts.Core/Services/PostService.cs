using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Core.Contracts;
using Posts.Core.Models;

namespace Posts.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserService _userService;
        private List<Post> Posts = new List<Post>();

        public PostService(IPostRepository postRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _userService = userService;
            ReadPosts();
        }

        public void ReadPosts()
        {
            Posts = _postRepository.GetPosts(GetUsers());
        }

        public string AddPost(Post post)
        {
            _postRepository.AddPost(post);
            Posts.Add(post);
            return "Post has been added";
        }

        public string DeletePost(Post post)
        {
            _postRepository.DeletePost(post);
            Posts.Remove(post);
            return "Post has been deleted";
        }

        public List<Post> GetPosts()
        {
            return Posts;
        }

        public string AddUser(User user)
        {
            return _userService.AddUser(user);
        }

        public string DeleteUser(User user)
        {
            _userService.DeleteUser(user);
            List<Post> posts = new List<Post>();
            foreach (Post post in Posts)
            {
                if (post.TheUser != user)
                    posts.Add(post);
            }
            Posts = posts;
            return "User has been deleted";
        }

        public List<User> GetUsers()
        {
            return _userService.GetUsers();
        }
    }
}
