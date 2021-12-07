using NUnit.Framework;

namespace Gateway.Test
{
    public class Gateway_AccountDataAccess_CheckIfAccountIsVerified
    {
        [Test]
        public void CheckIfAccountIsVerified_AccountVerified_ReturnsTrue()
        {
            // Arrange
            const string email = "ThisAccountDoesExist@windesheim.nl";

            // Act
            bool result = AccountDataAccess.CheckIfAccountIsVerified(email);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIfAccountIsVerified_AccountNotVerified_ReturnsFalse()
        {
            // Arrange
            const string email = "ThisUnverifiedAccountDoesExist@windesheim.nl";

            // Act
            bool result = AccountDataAccess.CheckIfAccountIsVerified(email);

            // Assert
            Assert.IsFalse(result);
        }
    }
}