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
            Account.authenticated = false;
            Account.email = null;
            Account.password = null;
            Account.passwordStrength = null;
            Account.userID = null;
            Account.verificationCode = null;
            Account.verifyPassword = null;
        }
    }
}