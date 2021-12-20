using NUnit.Framework;

namespace Gateway.Test
{
    public class Gateway_AccountDataAccess_CheckIfVerificationCodeMatches
    {
        [Test]
        public void CheckIfVerificationCodeMatches_ValidVerificationCode_ReturnsTrue()
        {
            // Arrange
            const string email = "ThisAccountDoesExist@wafoe.nl";
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
            const string email = "ThisAccountDoesExist@wafoe.nl";
            const string verificationCode = "000000";

            // Act
            bool result = AccountDataAccess.CheckIfVerificationCodeMatches(verificationCode, email);

            // Assert
            Assert.IsFalse(result);
        }
    }
}