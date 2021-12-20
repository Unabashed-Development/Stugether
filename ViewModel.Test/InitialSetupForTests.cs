using Gateway;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    [SetUpFixture]
    public class InitialSetupForTests
    {
        /// <summary>
        /// Initializes the SSH connection before running all other tests.
        /// </summary>
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            SSHService.Initialize();
        }

        /// <summary>
        /// Clears the fields in account to not mess up tests.
        /// </summary>
        public static void ClearFieldsInAccount()
        {
            Account.Authenticated = false;
            Account.Email = null;
            Account.Password = null;
            Account.PasswordStrength = null;
            Account.UserID = null;
            Account.VerificationCode = null;
            Account.VerifyPassword = null;
        }
    }
}