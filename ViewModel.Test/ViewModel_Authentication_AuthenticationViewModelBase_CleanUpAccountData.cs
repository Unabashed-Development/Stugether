using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Authentication_AuthenticationViewModelBase_CleanUpAccountData
    {
        [SetUp]
        public void Setup()
        {
            Account.Email = "test@mail.com";
            Account.Password = "TestPassword";
            Account.VerifyPassword = "TestPassword";
            Account.PasswordStrength = 5;
            Account.VerificationCode = "123456";
            Account.Authenticated = true;
        }

        [Test]
        public void CleanUpAccountData_CleanedUp_Successful()
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.NavigationStore()); // Setup one of the authentication view models

            // Act
            registerViewModel.CleanUpAccountData();

            // Assert
            Assert.IsNotNull(Account.Email);
            Assert.IsNull(Account.Password);
            Assert.IsNull(Account.VerifyPassword);
            Assert.IsNull(Account.PasswordStrength);
            Assert.IsNull(Account.VerificationCode);
            Assert.IsNotNull(Account.Authenticated);
        }

        public void CleanUpAccountData_NotCleanedUp_Succesful()
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.NavigationStore()); // Setup one of the authentication view models

            // Assert
            Assert.IsNotNull(Account.Email);
            Assert.IsNotNull(Account.Password);
            Assert.IsNotNull(Account.VerifyPassword);
            Assert.IsNotNull(Account.PasswordStrength);
            Assert.IsNotNull(Account.VerificationCode);
            Assert.IsNotNull(Account.Authenticated);
        }
    }
}