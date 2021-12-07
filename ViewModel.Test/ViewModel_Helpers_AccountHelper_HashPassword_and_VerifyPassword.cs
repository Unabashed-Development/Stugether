using NUnit.Framework;
using ViewModel.Helpers;

namespace ViewModel.Test
{
    public class ViewModel_Helpers_AccountHelper_HashPassword_and_VerifyPassword
    {
        /// <summary>
        /// This test tests HashPassword as the main functionality, but also uses VerifyPassword. Test only succeeds if both are working.
        /// </summary>
        [Test]
        public void HashPassword_and_VerifyPassword_String_ReturnsHashedString()
        {
            // Arrange
            const string unhashed = "HashMe";

            // Act
            string hashed = AccountHelper.HashPassword(unhashed);

            // Assert
            Assert.AreEqual('$', hashed[0], "The first character of a hash should start with $");
            Assert.IsTrue(AccountHelper.VerifyPassword(unhashed, hashed), "The hashed password should be the unhashed password");
        }
    }
}