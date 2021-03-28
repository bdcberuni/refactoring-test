using System;  
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp
{
    public static class UserDataAccess
    {
        public static IUserRepository _userRepository;

        public static void AddUser(User user)
        {
            _userRepository.AddUser(user);
        }
    }
}