using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly UserServiceAdapter _userServiceAdapter;
      
        public UserService()
        {
            _userServiceAdapter = UserServiceAdapter.GetInstance();
        }
        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            return _userServiceAdapter.AddUser(firname, surname, email, dateOfBirth, clientId);
        }
    }
}