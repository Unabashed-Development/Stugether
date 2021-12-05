using NUnit.Framework;

namespace Gateway.Test
{
    [SetUpFixture]
    public class InitializeSshBeforeRunningTests
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            SSHService.Initialize();
        }
    }
}