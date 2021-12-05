using NUnit.Framework;
using System;

namespace Gateway.Test
{
    public class Gateway_AccountDataAccess_CreateAccount
    {
        [Test]
        public void CreateAccount_NonExistingAccount_ThrowsNoException()
        {
            // Arrange
            Random random = new Random((int)DateTime.UtcNow.Ticks); // Generate random email and password to basically ensure it does not exist already in the database

            string email = random.Next().ToString() + random.Next().ToString();
            string password = random.Next().ToString() + random.Next().ToString();
            const string verificationCode = "123456";

            // Act
            static void HasSuccessfullyRegistered(string email, string password)
            {
                try
                {
                    AccountDataAccess.CreateAccount(email, password, verificationCode);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // Assert
            Assert.DoesNotThrow(() => HasSuccessfullyRegistered(email, password));
        }
    }
}