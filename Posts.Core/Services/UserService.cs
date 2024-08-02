using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Core.Contracts;
using Posts.Core.Models;

namespace Posts.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private List<User> Users = new List<User>();

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            ReadUsers();
        }

        public void ReadUsers()
        {
            Users = _userRepository.GetUsers();
        }

        public string AddUser(User user)
        {
            foreach (User theUser in Users)
            {
                if (theUser.Name.ToLower() == user.Name.ToLower() && theUser.Email == user.Email)
                    return "A user with such name and email already exists";
            }
            _userRepository.AddUser(user);
            Users.Add(user);
            return "User has been added";
        }

        public void DeleteUser(User user)
        {
            _userRepository.DeleteUser(user);
            Users.Remove(user);
        }

        public List<User> GetUsers()
        {
            return Users;
        }
    }
}
