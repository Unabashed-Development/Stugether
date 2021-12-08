using WinSCP;

namespace Gateway.Services
{
    public class SFTPService
    {
        /// <summary>
        /// Sets up the SSH/SFTP session options for connecting to the SFTP server.
        /// </summary>
        /// <returns>The SessionOptions usable for doing things with the SFTP server.</returns>
        public static SessionOptions GenerateSessionOptions()
        {
            // Set up session options
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = "145.44.233.153",
                UserName = "stugether_user",
                Password = "WachtwoordzinV00rStugether!",
                SshHostKeyFingerprint = "ssh-ed25519 255 zlXRHvsJRpS5YhfQawSmT8Op7JlWpZo335LVx99LEO4=",
            };

            sessionOptions.AddRawSettings("FSProtocol", "2");

            return sessionOptions;
        }
    }
}
