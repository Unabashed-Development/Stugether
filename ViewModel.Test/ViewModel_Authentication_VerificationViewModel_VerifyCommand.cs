using Gateway;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Authentication_VerificationViewModel_VerifyCommand
    {
        [Test]
        public void VerifyCommand_VerificationCodeDoesNotMatch_SetsCorrectErrorMessage()
        {
            // Arrange
            InitialSetupForTests.ClearFieldsInAccount();
            VerificationViewModel verificationViewModel = new VerificationViewModel(new Stores.NavigationStore())
            {
                Email = "ThisAccountDoesExist@wafoe.nl",
                VerificationCode = "654321" // The verification code in the database is "123456"
            };

            // Act
            verificationViewModel.VerifyCommand.Execute(null);

            // Assert
            Assert.IsNotNull(verificationViewModel.ErrorMessage);
            Assert.AreEqual("De verificatie code klopt niet.", verificationViewModel.ErrorMessage);
        }

        [Test]
        public void VerifyCommand_VerificationCodeMatches_LogsUserInAndSetsCorrectOutput()
        {
            // Arrange
            InitialSetupForTests.ClearFieldsInAccount();
            VerificationViewModel verificationViewModel = new VerificationViewModel(new Stores.NavigationStore())
            {
                Email = "ThisAccountDoesExist@wafoe.nl",
                VerificationCode = "123456" // The verification code in the database is "123456"
            };

            // Act
            verificationViewModel.VerifyCommand.Execute(null);

            // Assert
            Assert.IsNull(verificationViewModel.ErrorMessage);
            Assert.IsTrue(Account.authenticated);
            Assert.AreEqual(AccountDataAccess.GetUserIDFromAccount(verificationViewModel.Email), Account.userID);
        }
    }
}