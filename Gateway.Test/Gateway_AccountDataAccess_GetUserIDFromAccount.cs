using NUnit.Framework;
using System;

namespace Gateway.Test
{
    public class Gateway_AccountDataAccess_GetUserIDFromAccount
    {
        [Test]
        public void GetUserIDFromAccount_ExistingUserID_ReturnsCorrectID()
        {
            // Arrange
            const string email = "ThisAccountDoesExist@wafoe.nl";

            // Act
            int userID = AccountDataAccess.GetUserIDFromAccount(email);

            // Assert
            Assert.AreEqual(1, userID);
        }
    }
}