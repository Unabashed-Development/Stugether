using NUnit.Framework;

namespace Gateway.Test
{
    public class Gateway_Services_SSHService_Initialize
    {
        [Test]
        public void Initialize_IsConnected_ReturnsTrue()
        {
            // Arrange
            // No arranging needed, as InitializeSshBeforeRunningTests arranges the initial SSH connection 

            // Act
            bool isConnected = SSHService.SshConnection.IsConnected;

            // Assert
            Assert.IsTrue(isConnected);
        }
    }
}