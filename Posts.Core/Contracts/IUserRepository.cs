﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Core.Models;

namespace Posts.Core.Contracts
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        void AddUser(User user);
        void DeleteUser(User user);
    }
}
