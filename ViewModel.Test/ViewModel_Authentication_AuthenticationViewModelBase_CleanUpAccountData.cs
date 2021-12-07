using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Authentication_AuthenticationViewModelBase_CleanUpAccountData
    {
        [SetUp]
        public void Setup()
        {
            Account.email = "test@mail.com";
            Account.password = "TestPassword";
            Account.verifyPassword = "TestPassword";
            Account.passwordStrength = 5;
            Account.verificationCode = "123456";
            Account.authenticated = true;
        }

        [Test]
        public void CleanUpAccountData_CleanedUp_Successful()
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.AuthenticationNavigationStore()); // Setup one of the authentication view models

            // Act
            registerViewModel.CleanUpAccountData();

            // Assert
            Assert.IsNotNull(Account.email);
            Assert.IsNull(Account.password);
            Assert.IsNull(Account.verifyPassword);
            Assert.IsNull(Account.passwordStrength);
            Assert.IsNull(Account.verificationCode);
            Assert.IsNotNull(Account.authenticated);
        }

        public void CleanUpAccountData_NotCleanedUp_Succesful()
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.AuthenticationNavigationStore()); // Setup one of the authentication view models

            // Assert
            Assert.IsNotNull(Account.email);
            Assert.IsNotNull(Account.password);
            Assert.IsNotNull(Account.verifyPassword);
            Assert.IsNotNull(Account.passwordStrength);
            Assert.IsNotNull(Account.verificationCode);
            Assert.IsNotNull(Account.authenticated);
        }
    }
}