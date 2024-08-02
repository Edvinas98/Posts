using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Core.Contracts;
using Posts.Core.Models;
using Posts.Core.Services;

namespace Posts.Services
{
    public class MenuUI
    {
        private IPostService _postService;
        public MenuUI(IPostService postService)
        {
            _postService = postService;
        }

        public void LaunchMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Open user menu");
                Console.WriteLine("2. Open post menu");
                Console.WriteLine("0. Close");
                Console.Write("Enter your choice: ");
                GetInput(out string choice);
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        OpenUserMenu();
                        break;
                    case "2":
                        OpenPostMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Wrong choice");
                        Console.WriteLine();
                        break;
                }
            }
        }

        public void OpenUserMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Show all users");
                Console.WriteLine("2. Add user");
                Console.WriteLine("3. Delete user");
                Console.WriteLine("0. Back");
                Console.Write("Enter your choice: ");
                GetInput(out string choice);
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        List<User> users = _postService.GetUsers();
                        if (users.Count == 0)
                        {
                            Console.WriteLine("There are no users");
                            break;
                        }
                        foreach (User user in users)
                        {
                            Console.WriteLine(user);
                        }
                        break;
                    case "2":
                        Console.Write("Enter user name: ");
                        GetInput(out string name);
                        Console.Write("Enter email: ");
                        GetInput(out string email);
                        Console.WriteLine();
                        Console.WriteLine(_postService.AddUser(new User(name, email)));
                        break;
                    case "3":
                        users = _postService.GetUsers();
                        if (users.Count == 0)
                        {
                            Console.WriteLine("There are no users");
                            break;
                        }
                        for (int i = 0; i < users.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {users[i]}");
                        }
                        Console.WriteLine("0. Cancel");
                        Console.Write("Select a user to delete: ");
                        GetInput(out int index, 0);
                        if (index == 0 || index > users.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Deleting has been canceled");
                            break;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(_postService.DeleteUser(users[index - 1]));
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Wrong choice");
                        break;
                }
                Console.WriteLine();
            }
        }

        public void OpenPostMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Show all posts");
                Console.WriteLine("2. Add post");
                Console.WriteLine("3. Delete post");
                Console.WriteLine("0. Back");
                Console.Write("Enter your choice: ");
                GetInput(out string choice);
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        List<Post> posts = _postService.GetPosts();
                        if (posts.Count == 0)
                        {
                            Console.WriteLine("There are no posts");
                            break;
                        }
                        foreach (Post post in posts)
                        {
                            Console.WriteLine(post);
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        List<User> users = _postService.GetUsers();
                        if (users.Count == 0)
                        {
                            Console.WriteLine("There are no users to select from");
                            break;
                        }
                        for (int i = 0; i < users.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {users[i]}");
                        }
                        Console.WriteLine("0. Cancel");
                        Console.Write("Select a post owner: ");
                        GetInput(out int index, 0);
                        if (index == 0 || index > users.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Post creation has been canceled");
                            break;
                        }
                        User user = users[index - 1];
                        Console.Write("Enter post title: ");
                        GetInput(out string title, 100);
                        Console.Write("Enter post content: ");
                        GetInput(out string content, 2000);
                        Console.WriteLine();
                        Console.WriteLine(_postService.AddPost(new Post(user, title, content)));
                        break;
                    case "3":
                        posts = _postService.GetPosts();
                        if (posts.Count == 0)
                        {
                            Console.WriteLine("There are no posts");
                            break;
                        }
                        for (int i = 0; i < posts.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.".PadRight(4) + $" {posts[i]}");
                            Console.WriteLine();
                        }
                        Console.WriteLine("0. Cancel");
                        Console.Write("Select a post to delete: ");
                        GetInput(out index, 0);
                        if (index == 0 || index > posts.Count)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Deleting has been canceled");
                            break;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(_postService.DeletePost(posts[index - 1]));
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Wrong choice");
                        break;
                }
                Console.WriteLine();
            }
        }

        public static void GetInput(out string input)
        {
            while (true)
            {
                input = Console.ReadLine() ?? string.Empty;
                if (input != "")
                    return;
                else
                    Console.Write("Wrong input, try again: ");
            }
        }

        public static void GetInput(out string input, int length)
        {
            while (true)
            {
                input = Console.ReadLine() ?? string.Empty;
                if (input.Length <= length)
                    return;
                else
                    Console.Write($"Max {length} characters! Try again: ");
            }
        }

        public static void GetInput(out int input, int minValue)
        {
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out input) || input < minValue)
                    Console.Write("Wrong input, try again: ");
                else
                    return;
            }
        }
    }
}
