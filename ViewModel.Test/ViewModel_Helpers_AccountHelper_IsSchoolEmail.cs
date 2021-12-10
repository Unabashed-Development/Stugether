using NUnit.Framework;
using ViewModel.Helpers;

namespace ViewModel.Test
{
    public class ViewModel_Helpers_AccountHelper_HashPassword
    {
        [TestCase("")]
        [TestCase("a")]
        [TestCase("abcdefghijklmnopqrstuvwxyz")]
        [TestCase("a@a")]
        [TestCase("a@a.nl")]
        [TestCase("hello@welcome.com")]
        [TestCase("@windesheim")]
        [TestCase("hello@welcome.a.b.c.d.e.f.g")]
        [TestCase("hello-com.test@welcome.a.b.c.d.e.f.g")]
        public void IsSchoolEmail_NoSchoolMail_ReturnsFalse(string email)
        {
            // Act
            bool schoolMail = AccountHelper.IsSchoolEmail(email);

            // Assert
            Assert.IsFalse(schoolMail);
        }

        [TestCase("test@windesheim.nl")]
        [TestCase("test@student.windesheim.nl")]
        [TestCase("@student.windesheim.nl")]
        [TestCase("@windesheim.nl")]
        [TestCase("@windesheim.com")]
        public void IsSchoolEmail_IsSchoolMail_ReturnsTrue(string email)
        {
            // Act
            bool schoolMail = AccountHelper.IsSchoolEmail(email);

            // Assert
            Assert.IsTrue(schoolMail);
        }
    }
}