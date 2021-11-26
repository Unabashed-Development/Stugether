using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Helpers
{
    public static class AccountHelper
    {
        #region Fields
        /// <summary>
        /// This field is used to improve performance. Encapsulates the EmailAddressAttribute. 
        /// </summary>
        private static EmailAddressAttribute _emailChecker;
        #endregion

        #region Methods
        /// <summary>
        /// Hashes a password with BCrypt using a Salt.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>The hashed password.</returns>
        public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        /// <summary>
        /// Verifies a hashed and salted password against an unhashed password.
        /// </summary>
        /// <param name="unhashedPassword">The password that is not hashed.</param>
        /// <param name="hashedPassword">The hashed and salted password.</param>
        /// <returns>True if the unhashedPassword matches the hashedPassword and false if not.</returns>
        public static bool VerifyPassword(string unhashedPassword, string hashedPassword) => BCrypt.Net.BCrypt.Verify(unhashedPassword, hashedPassword);

        /// <summary>
        /// Gets the current time, exact to the millisecond.
        /// </summary>
        /// <returns>A DateTime object with the current time.</returns>
        public static DateTime GetCurrentTime() => DateTime.Now;

        /// <summary>
        /// Checks if the e-mail entered is a valid email format.
        /// </summary>
        /// <param name="email">The e-mail to be checked.</param>
        /// <returns>True if the email is a valid format and false if not.</returns>
        public static bool IsValidEmail(string email)
        {
            if (_emailChecker == null)
            {
                _emailChecker = new EmailAddressAttribute();
            }
            return _emailChecker.IsValid(email);
        }
        #endregion
    }
}
