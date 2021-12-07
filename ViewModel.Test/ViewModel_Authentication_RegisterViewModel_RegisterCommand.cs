using Gateway;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Authentication_RegisterViewModel_RegisterCommand
    {
        [TestCase(null, null, null)]
        [TestCase("test@test.com", null, null)]
        [TestCase("test@test.com", "TestPassword", null)]
        [TestCase("test@test.com", null, "TestPassword")]
        [TestCase(null, "TestPassword", null)]
        [TestCase(null, "TestPassword", "TestPassword")]
        [TestCase(null, null, "TestPassword")]
        [TestCase("", "", "")]
        [TestCase("test@test.com", "", "")]
        [TestCase("test@test.com", "TestPassword", "")]
        [TestCase("test@test.com", "", "TestPassword")]
        [TestCase("", "TestPassword", "")]
        [TestCase("", "TestPassword", "TestPassword")]
        [TestCase("", "", "TestPassword")]
        public void RegisterCommand_NotEveryFieldFilled_SetsCorrectErrorMessage(string email, string password, string verifyPassword)
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.AuthenticationNavigationStore())
            {
                Email = email,
                Password = password,
                VerifyPassword = verifyPassword
            };

            // Act
            registerViewModel.RegisterCommand.Execute(null);

            // Assert
            Assert.IsNotNull(registerViewModel.ErrorMessage);
            Assert.AreEqual("Niet alle velden zijn ingevuld.", registerViewModel.ErrorMessage);
        }

        [TestCase("ThisIsNotAnEmail")]
        [TestCase("ThisIsNotASchoolEmail@qwertyuiop.nl")]
        public void RegisterCommand_InvalidSchoolEmail_SetsCorrectErrorMessage(string email)
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.AuthenticationNavigationStore())
            {
                Email = email,
                Password = "SomethingToPreventNotAllFieldsEnteredError",
                VerifyPassword = "SomethingToPreventNotAllFieldsEnteredError"
            };

            // Act
            registerViewModel.RegisterCommand.Execute(null);

            // Assert
            Assert.IsNotNull(registerViewModel.ErrorMessage);
            Assert.AreEqual("Dit e-mailadres is geen geldig school adres.", registerViewModel.ErrorMessage);
        }

        [TestCase("a")]
        [TestCase("abc")]
        [TestCase("#")]
        [TestCase("#$%^&")]
        [TestCase("123")]
        [TestCase("abcdefghijklmnopqrstuvwxyz")]
        [TestCase("Abcdefghijklmnopqrstuvwxyz")]
        public void RegisterCommand_InsufficientPassword_SetsCorrectErrorMessage(string password)
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.AuthenticationNavigationStore())
            {
                Email = "ThisIsASchoolEmail@windesheim.nl",
                Password = password,
                VerifyPassword = password
            };

            // Act
            registerViewModel.RegisterCommand.Execute(null);

            // Assert
            Assert.IsNotNull(registerViewModel.ErrorMessage);
            Assert.AreEqual("Je wachtwoord voldoet niet aan de minimale eisen.", registerViewModel.ErrorMessage);
        }

        [TestCase("ThisP4ssw@rdIsSufficient", "test")]
        [TestCase("ThisP4ssw@rdIsSufficient", "abc123DEF!@#")]
        public void RegisterCommand_PasswordsSufficientButDoesNotMatch_SetsCorrectErrorMessage(string password, string verifyPasword)
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.AuthenticationNavigationStore())
            {
                Email = "ThisIsASchoolEmail@windesheim.nl",
                Password = password,
                VerifyPassword = verifyPasword
            };

            // Act
            registerViewModel.RegisterCommand.Execute(null);

            // Assert
            Assert.IsNotNull(registerViewModel.ErrorMessage);
            Assert.AreEqual("Je wachtwoorden komen niet overeen met elkaar.", registerViewModel.ErrorMessage);
        }

        [Test]
        public void RegisterCommand_AccountAlreadyExists_SetsCorrectErrorMessage()
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.AuthenticationNavigationStore())
            {
                Email = "ThisAccountDoesExist@windesheim.nl",
                Password = "ThisP4ssw@rdIsSufficient",
                VerifyPassword = "ThisP4ssw@rdIsSufficient"
            };

            // Act
            registerViewModel.RegisterCommand.Execute(null);

            // Assert
            Assert.IsNotNull(registerViewModel.ErrorMessage);
            Assert.AreEqual("Dit account bestaat al.", registerViewModel.ErrorMessage);
        }

        [Test]
        public void RegisterCommand_NoProblems_CreatesAccountAndGivesCorrectOutput()
        {
            // Arrange
            RegisterViewModel registerViewModel = new RegisterViewModel(new Stores.AuthenticationNavigationStore())
            {
                Email = "ThisIsANewSchoolEmail@wafoe.nl",
                Password = "ThisP4ssw@rdIsSufficient",
                VerifyPassword = "ThisP4ssw@rdIsSufficient"
            };

            // Act
            registerViewModel.RegisterCommand.Execute(null);

            // Clean up
            AccountDataAccess.DeleteAccount(registerViewModel.Email);

            // Assert
            Assert.IsNull(registerViewModel.ErrorMessage);
            Assert.IsNull(registerViewModel.VerificationCode);
            Assert.IsNull(registerViewModel.Password);
            Assert.IsNull(registerViewModel.VerifyPassword);
            Assert.IsNull(registerViewModel.PasswordStrength);
            Assert.IsNotNull(registerViewModel.Email);
            Assert.IsNull(Account.userID);
            Assert.IsFalse(Account.authenticated);
        }
    }
}