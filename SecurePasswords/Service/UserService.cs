using SecurePasswords.Models;
using SecurePasswords.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace SecurePasswords.Service
{
    internal interface IUserService
    {
        bool CreateUser(User user);
        User GetUserByLogin(string username, string password);
    }

    internal class UserService : IUserService
    {
        private readonly List<User> users = new List<User>();

        public bool CreateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Username))
            {
                return false;
            }
            if (users.Exists(u => u.Username == user.Username))
            {
                return false;
            }

            user.Salt = HashUtillity.GenerateSalt();
            user.Hash = HashUtillity.HashText(user.Password, user.Salt, 32);
            users.Add(user);
            return true;
        }

        public User GetUserByLogin(string username, string password)
        {
            User user = users.FindAll(u => u.Username == username)
                .SingleOrDefault();

            if (user == null)
            {
                throw new AuthenticationException("Invalid username");
            }

            if (user.FailedAttempts >= 5)
            {
                throw new AuthenticationException("Too many failed attempts");
            }

            byte[] hashToTest = HashUtillity.HashText(password, user.Salt, 32);
            
            if (!HashUtillity.CompareHashes(hashToTest, user.Hash))
            {
                user.FailedAttempts++;
                throw new AuthenticationException("Invalid password");
            }

            user.FailedAttempts = 0;
            return user;
        }
    }
}
