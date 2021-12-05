using System;

namespace Model
{
    public static class Account
    {
        public static string email;
        public static string password;
        public static string verifyPassword;
        public static int? passwordStrength; // Nullable
        public static string verificationCode;
        public static bool authenticated;
    }
}