using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway
{
    public class Account
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string HashPassword(string unhashedPassword) => BCrypt.Net.BCrypt.HashPassword(unhashedPassword);
        public bool VerifyPassword(string unhashedPassword, string hashedPassword) => BCrypt.Net.BCrypt.Verify(unhashedPassword, hashedPassword);
        public void CreateUserAccount(string email, string unhashedPassword)
        {

        }
    }
}
