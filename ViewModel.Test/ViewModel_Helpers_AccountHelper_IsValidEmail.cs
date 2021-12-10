using NUnit.Framework;
using ViewModel.Helpers;

namespace ViewModel.Test
{
    public class ViewModel_Helpers_AccountHelper_IsValidEmail
    {
        [TestCase("")]
        [TestCase("a")]
        [TestCase("abcdefghijklmnopqrstuvwxyz")]
        public void IsValidEmail_InvalidEmail_ReturnsFalse(string email)
        {
            // Act
            bool validEmail = AccountHelper.IsValidEmail(email);

            // Assert
            Assert.IsFalse(validEmail);
        }

        [TestCase("a@a")]
        [TestCase("a@a.nl")]
        [TestCase("hello@welcome.com")]
        [TestCase("hello@welcome.a.b.c.d.e.f.g")]
        [TestCase("hello-com.test@welcome.a.b.c.d.e.f.g")]
        [TestCase("test@windesheim.nl")]
        public void IsValidEmail_ValidEmail_ReturnsTrue(string email)
        {
            // Act
            bool validEmail = AccountHelper.IsValidEmail(email);

            // Assert
            Assert.IsTrue(validEmail);
        }
    }
}