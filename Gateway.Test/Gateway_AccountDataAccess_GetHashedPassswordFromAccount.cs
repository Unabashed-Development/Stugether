using NUnit.Framework;

namespace Gateway.Test
{
    public class Gateway_AccountDataAccess_GetHashedPassswordFromAccount
    {
        /// <summary>
        /// This test should return a value starting with a dollar sign, because that is the first char in a hashed and salted string.
        /// </summary>
        [Test]
        public void GetHashedPassswordFromAccount_ExistingAccount_ReturnsValueStartingWithDollarSign()
        {
            // Arrange
            const string email = "ThisAccountDoesExist@wafoe.nl";

            // Act
            string hashedPassword = AccountDataAccess.GetHashedPassswordFromAccount(email);
            bool startsWithDollarSign = hashedPassword[0] == '$';

            // Assert
            Assert.IsTrue(startsWithDollarSign);
        }
    }
}