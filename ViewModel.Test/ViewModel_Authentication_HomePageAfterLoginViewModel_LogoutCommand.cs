using Model;
using NUnit.Framework;
using ViewModel.HomePages;

namespace ViewModel.Test
{
    public class ViewModel_Authentication_HomePageAfterLoginViewModel_LogoutCommand
    {
        [SetUp]
        public void Setup()
        {
            InitialSetupForTests.ClearFieldsInAccount();
            Account.Email = "test@mail.com";
            Account.Authenticated = true;
            Account.UserID = 1;
        }

        [Test]
        public void LogoutCommand_LogsUserOut_UserLoggedOut()
        {
            // Arrange
            HomePageAfterLoginViewModel viewModel = new HomePageAfterLoginViewModel();

            // Act
            viewModel.LogoutCommand.Execute(null);

            // Assert
            Assert.IsNull(Account.Email);
            Assert.IsNull(Account.Password);
            Assert.IsNull(Account.VerifyPassword);
            Assert.IsNull(Account.PasswordStrength);
            Assert.IsNull(Account.VerificationCode);
            Assert.IsFalse(Account.Authenticated);
            Assert.IsNull(Account.UserID);
        }
    }
}