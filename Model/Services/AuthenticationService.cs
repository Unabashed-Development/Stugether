using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Services
{
    class AuthenticationService
    {
        public Account Login(string email, string password)
        {
            throw new NotImplementedException();
        }
        public bool Register(string email, string password, string confirmPassword)
        { 
            if (password == confirmPassword)
            {
                Account account = new Account()
                {
                    Email = email,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    DatePasswordChanged = DateTime.Now
                };
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
