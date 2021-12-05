using NUnit.Framework;
using System.Configuration;
using System.Reflection;

namespace Gateway.Test
{
    public class Gateway_AccountDataAccess_CheckIfVerificationCodeMatches
    {
        [Test]
        public void CheckIfVerificationCodeMatches_ValidVerificationCode_ReturnsTrue()
        {
            // Arrange
            const string email = "ThisAccountDoesExist";
            const string verificationCode = "123456";

            // Act
            bool result = AccountDataAccess.CheckIfVerificationCodeMatches(verificationCode, email);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIfVerificationCodeMatches_InvalidVerificationCode_ReturnsFalse()
        {
            // Arrange
            const string email = "ThisAccountDoesExist";
            const string verificationCode = "000000";

            // Act
            var ding = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configLocation = Assembly.GetEntryAssembly().Location;
            bool result = AccountDataAccess.CheckIfVerificationCodeMatches(verificationCode, email);

            // Assert
            Assert.IsFalse(result);
        }
    }
}