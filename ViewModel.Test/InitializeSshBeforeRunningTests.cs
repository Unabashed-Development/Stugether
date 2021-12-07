using Gateway;
using NUnit.Framework;

namespace ViewModel.Test
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