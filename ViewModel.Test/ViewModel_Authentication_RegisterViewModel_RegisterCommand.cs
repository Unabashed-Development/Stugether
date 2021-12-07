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
        public void RegisterCommand_NotEveryFieldFilled_SetsErrorMessage(string email, string password, string verifyPassword)
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
    }
}