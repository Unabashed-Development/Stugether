using Model;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Checks if the given email is a school email and compares it against a given list of email root domains. Assumes the email has at least '@' character.
        /// </summary>
        /// <param name="email">The e-mail to be checked.</param>
        /// <param name="allowedSchoolRootDomains">The list of school root domains.</param>
        /// <returns>True if the email is a valid school email and false if not.</returns>
        public static bool IsSchoolEmail(string email, List<string> allowedSchoolRootDomains)
        {
            if (email.Contains('@'))
            {
                string[] emailSplitAtAmpersand = email.Split('@'); // Split the domain in two halves. Example: hello@main.test.com > hello & main.test.com
                string rootDomainOfEmail = emailSplitAtAmpersand[1].ToLower();
                foreach (string s in allowedSchoolRootDomains)
                {
                    if (rootDomainOfEmail.EndsWith(s))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Gets all domain names from an IEnumerable of AllowedSchool domain arrays.
        /// </summary>
        /// <param name="allowedSchools">The IEnumerable of AllowedSchool classes.</param>
        /// <returns>A list of all domain names (root domain).</returns>
        public static List<string> RetrieveAllDomainNamesFromAllowedSchools(IEnumerable<AllowedSchool> allowedSchools)
        {
            List<string> list = new List<string>();
            foreach (AllowedSchool c in allowedSchools)
            {
                foreach (string s in c.Domains)
                {
                    list.Add(s);
                }
            }
            return list;
        }

        /// <summary>
        /// Generates a random verification code based on a hidden secret, current time and the e-mailaddress.
        /// </summary>
        /// <param name="email">The e-mail address used for the seed of the random code.</param>
        /// <returns>The 6-character verification code as a string.</returns>
        public static string GenerateVerificationCode(string email)
        {
            int hiddenSecret = 766822153;
            int currentTime = (int)DateTime.UtcNow.Ticks;
            int emailHashCode = email.GetHashCode();
            int average = (hiddenSecret + currentTime + emailHashCode) / 3;

            Random random = new Random(average);
            string verificationCode = random.Next(0, 999999).ToString();

            return verificationCode.Length switch // This switch adds zeroes if the verification code is shorter than 6 numbers
            {
                5 => verificationCode += "0",
                4 => verificationCode += "00",
                3 => verificationCode += "000",
                2 => verificationCode += "0000",
                1 => verificationCode += "00000",
                _ => verificationCode,
            };
        }
        #endregion
    }
}
