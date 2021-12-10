using System;
using System.Collections.Generic;

namespace Model
{
    public static class Account
    {
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string VerifyPassword { get; set; }
        public static int? PasswordStrength { get; set; } // Nullable
        public static string VerificationCode { get; set; }
        public static bool Authenticated { get; set; } = true;
        public static int? UserID { get; set; } = 3; // Nullable
        public static List<Profile> Matches { get; set; }
    }
}