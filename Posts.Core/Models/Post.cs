using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Core.Models
{
    public class Post
    {
        public User TheUser { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }


        public Post(User user, string title, string content)
        {
            TheUser = user;
            Title = title;
            Content = content;
        }

        public override string ToString()
        {
            return $"{TheUser.Name}".PadRight(20) + $" {Title}\n" + Content;
        }
    }
}
