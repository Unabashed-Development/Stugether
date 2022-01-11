using Gateway;
using Model;
using NUnit.Framework;
using System.Collections.Generic;
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
        [TestCase("@windesheim.com")]
        [TestCase("@windesheim")]
        [TestCase("hello@welcome.a.b.c.d.e.f.g")]
        [TestCase("hello-com.test@welcome.a.b.c.d.e.f.g")]
        public void IsSchoolEmail_NoSchoolMail_ReturnsFalse(string email)
        {
            // Arrange
            IEnumerable<AllowedSchool> allowedSchools = AllowedSchoolsDataAccess.APICallAllowedSchools();
            List<string> allowedSchoolRootDomains = AccountHelper.RetrieveAllDomainNamesFromAllowedSchools(allowedSchools);

            // Act
            bool schoolMail = AccountHelper.IsSchoolEmail(email, allowedSchoolRootDomains);

            // Assert
            Assert.IsFalse(schoolMail);
        }

        [TestCase("test@windesheim.nl")]
        [TestCase("test@student.windesheim.nl")]
        [TestCase("@student.windesheim.nl")]
        [TestCase("@windesheim.nl")]
        [TestCase("@uva.nl")]
        [TestCase("@abcdefg.uva.nl")]
        [TestCase("@hku.nl")]
        public void IsSchoolEmail_IsSchoolMail_ReturnsTrue(string email)
        {
            // Arrange
            IEnumerable<AllowedSchool> allowedSchools = AllowedSchoolsDataAccess.APICallAllowedSchools();
            List<string> allowedSchoolRootDomains = AccountHelper.RetrieveAllDomainNamesFromAllowedSchools(allowedSchools);

            // Act
            bool schoolMail = AccountHelper.IsSchoolEmail(email, allowedSchoolRootDomains);

            // Assert
            Assert.IsTrue(schoolMail);
        }
    }
}