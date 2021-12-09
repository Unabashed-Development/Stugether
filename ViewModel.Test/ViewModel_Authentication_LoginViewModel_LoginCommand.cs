using Gateway;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Authentication_LoginViewModel_LoginCommand
    {
        [TestCase(null, null)]
        [TestCase("test@test.com", null)]
        [TestCase(null, "TestPassword")]
        [TestCase("", "")]
        [TestCase("test@test.com", "")]
        [TestCase("", "TestPassword")]
        public void LoginCommand_NotEveryFieldFilled_SetsCorrectErrorMessage(string email, string password)
        {
            // Arrange
            InitialSetupForTests.ClearFieldsInAccount();
            LoginViewModel loginViewModel = new LoginViewModel(new Stores.NavigationStore())
            {
                Email = email,
                Password = password
            };

            // Act
            loginViewModel.LoginCommand.Execute(null);

            // Assert
            Assert.IsNotNull(loginViewModel.ErrorMessage);
            Assert.AreEqual("Niet alle velden zijn ingevuld.", loginViewModel.ErrorMessage);
        }

        [Test]
        public void LoginCommand_InvalidEmail_SetsCorrectErrorMessage()
        {
            // Arrange
            InitialSetupForTests.ClearFieldsInAccount();
            LoginViewModel loginViewModel = new LoginViewModel(new Stores.NavigationStore())
            {
                Email = "ThisIsNotAnEmail",
                Password = "SomethingToPreventNotAllFieldsEnteredError"
            };

            // Act
            loginViewModel.LoginCommand.Execute(null);

            // Assert
            Assert.IsNotNull(loginViewModel.ErrorMessage);
            Assert.AreEqual("Dit e-mailadres is niet geldig.", loginViewModel.ErrorMessage);
        }

        [Test]
        public void RegisterCommand_AccountDoesNotExist_SetsCorrectErrorMessage()
        {
            // Arrange
            InitialSetupForTests.ClearFieldsInAccount();
            LoginViewModel loginViewModel = new LoginViewModel(new Stores.NavigationStore())
            {
                Email = "ThisAccountDoesNotExist@wafoe.nl",
                Password = "SomethingToPreventNotAllFieldsEnteredError"
            };

            // Act
            loginViewModel.LoginCommand.Execute(null);

            // Assert
            Assert.IsNotNull(loginViewModel.ErrorMessage);
            Assert.AreEqual("Dit account bestaat niet.", loginViewModel.ErrorMessage);
        }

        [Test]
        public void RegisterCommand_AccountExistButCredentialsInvalid_SetsCorrectErrorMessage()
        {
            // Arrange
            InitialSetupForTests.ClearFieldsInAccount();
            LoginViewModel loginViewModel = new LoginViewModel(new Stores.NavigationStore())
            {
                Email = "ThisAccountDoesExist@wafoe.nl",
                Password = "InvalidPassword"
            };

            // Act
            loginViewModel.LoginCommand.Execute(null);

            // Assert
            Assert.IsNotNull(loginViewModel.ErrorMessage);
            Assert.AreEqual("Je inloggegevens zijn onjuist.", loginViewModel.ErrorMessage);
        }

        [Test]
        public void RegisterCommand_AccountExistAndValidCredentialsButNotVerified_SetsCorrectErrorMessage()
        {
            // Arrange
            InitialSetupForTests.ClearFieldsInAccount();
            LoginViewModel loginViewModel = new LoginViewModel(new Stores.NavigationStore())
            {
                Email = "ThisUnverifiedAccountDoesExist@wafoe.nl",
                Password = "ValidPassword"
            };

            // Act
            loginViewModel.LoginCommand.Execute(null);

            // Assert
            Assert.IsNotNull(loginViewModel.ErrorMessage);
            Assert.AreEqual("Je account is nog niet geverifieerd.", loginViewModel.ErrorMessage);
        }

        [Test]
        public void RegisterCommand_AccountExistAndValidCredentials_LogsUserInAndGivesCorrectOutput()
        {
            // Arrange
            InitialSetupForTests.ClearFieldsInAccount();
            LoginViewModel loginViewModel = new LoginViewModel(new Stores.NavigationStore())
            {
                Email = "ThisAccountDoesExist@wafoe.nl",
                Password = "ValidPassword"
            };

            // Act
            loginViewModel.LoginCommand.Execute(null);

            // Assert
            Assert.IsNull(loginViewModel.ErrorMessage);
            Assert.IsNull(loginViewModel.VerificationCode);
            Assert.IsNull(loginViewModel.Password);
            Assert.IsNull(loginViewModel.VerifyPassword);
            Assert.IsNull(loginViewModel.PasswordStrength);
            Assert.AreEqual("ThisAccountDoesExist@wafoe.nl", loginViewModel.Email);
            Assert.AreEqual(AccountDataAccess.GetUserIDFromAccount(loginViewModel.Email), Account.UserID);
            Assert.IsTrue(Account.Authenticated);
        }
    }
}