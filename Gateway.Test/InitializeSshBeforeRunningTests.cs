using NUnit.Framework;

namespace Gateway.Test
{
    [SetUpFixture]
    public class InitializeSshBeforeRunningTests
    {
        /// <summary>
        /// Initializes the SSH connection before running all other tests.
        /// </summary>
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            SSHService.Initialize();
        }
    }
}