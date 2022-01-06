using System.Threading;
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
        public static bool Authenticated { get; set; }
        public static int? UserID { get; set; } // Nullable
        public static List<Profile> Likes { get; set; }
        public static List<Profile> Matches { get; set; }
        public static Dictionary<string, Timer> BackgroundThreads { get; set; } = new Dictionary<string, Timer>();
        public static NotificationSettings NotificationSettings { get; set; }
    }
}