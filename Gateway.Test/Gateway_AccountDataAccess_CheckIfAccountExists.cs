using NUnit.Framework;

namespace Gateway.Test
{
    public class Gateway_AccountDataAccess_CheckIfAccountExists
    {
        [Test]
        public void CheckIfAccountExists_ExistingAccount_ReturnsTrue()
        {
            // Arrange
            const string email = "ThisAccountDoesExist@wafoe.nl";

            // Act
            bool result = AccountDataAccess.CheckIfAccountExists(email);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIfAccountExists_NonExistingAccount_ReturnsFalse()
        {
            // Arrange
            const string email = "ThisAccountDoesNotExist";

            // Act
            bool result = AccountDataAccess.CheckIfAccountExists(email);

            // Assert
            Assert.IsFalse(result);
        }
    }
}