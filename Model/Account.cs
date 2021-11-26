using System;

namespace Model
{
    public class Account
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DatePasswordChanged { get; set; }
    }
}