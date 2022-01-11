using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Gateway
{
    public static class SSHService // Source: https://mysqlconnector.net/tutorials/connect-ssh/
    {
        #region Properties
        public static SshClient SshConnection { get; private set; }
        public static uint SshPort { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Sets up a SSH connection to a remote SSH server. Needs: dotnet add package SSH.NET
        /// </summary>
        /// <param name="sshHostName">Hostname of the SSH server to connect to.</param>
        /// <param name="sshUserName">Username of the SSH server to connect to.</param>
        /// <param name="sshPassword">Password of the SSH user on the SSH server.</param>
        /// <param name="sshKeyFile">Optional certificate file instead of a password.</param>
        /// <param name="sshPassPhrase">Optional pass phrase instead of a password.</param>
        /// <param name="sshPort">Optional change of the SSH port on the remote server.</param>
        /// <param name="databaseServer">Optional change of the local database server address.</param>
        /// <param name="databasePort">Optional change of the IP address of the remote SSH server.</param>
        /// <returns>
        /// A tuple of the SshClient and the randomized local port.
        /// </returns>
        private static Tuple<SshClient, uint> ConnectSsh(string sshHostName, string sshUserName, string sshPassword = null, string sshKeyFile = null,
        string sshPassPhrase = null, int sshPort = 22, string databaseServer = "localhost", int databasePort = 1433)
        {
            // Check arguments
            if (string.IsNullOrEmpty(sshHostName))
            {
                throw new ArgumentException($"{nameof(sshHostName)} must be specified.", nameof(sshHostName));
            }

            if (string.IsNullOrEmpty(sshHostName))
            {
                throw new ArgumentException($"{nameof(sshUserName)} must be specified.", nameof(sshUserName));
            }

            if (string.IsNullOrEmpty(sshPassword) && string.IsNullOrEmpty(sshKeyFile))
            {
                throw new ArgumentException($"One of {nameof(sshPassword)} and {nameof(sshKeyFile)} must be specified.");
            }

            if (string.IsNullOrEmpty(databaseServer))
            {
                throw new ArgumentException($"{nameof(databaseServer)} must be specified.", nameof(databaseServer));
            }

            // Define the authentication methods to use (in order)
            List<AuthenticationMethod> authenticationMethods = new List<AuthenticationMethod>();
            if (!string.IsNullOrEmpty(sshKeyFile))
            {
                authenticationMethods.Add(new PrivateKeyAuthenticationMethod(sshUserName,
                    new PrivateKeyFile(sshKeyFile, string.IsNullOrEmpty(sshPassPhrase) ? null : sshPassPhrase)));
            }
            if (!string.IsNullOrEmpty(sshPassword))
            {
                authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword));
            }

            // Connect to the SSH server
            SshClient sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
            sshClient.Connect();

            // Forward a local port to the database server and port, using the SSH server
            //ForwardedPortLocal forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort); // Uses a random port
            ForwardedPortLocal forwardedPort = new ForwardedPortLocal("127.0.0.1", (uint)databasePort, databaseServer, (uint)databasePort); // Makes port same as database port
            sshClient.AddForwardedPort(forwardedPort);
            forwardedPort.Start();

            return Tuple.Create(sshClient, forwardedPort.BoundPort);
        }

        /// <summary>
        /// Sets up an SSL connection and binds the SshClient and SshPort to properties.
        /// </summary>
        public static void Initialize()
        {
            // Load the embedded resource with assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(@"DatabaseInfo.txt"));

            // Read the file per line
            List<string> list = new List<string>();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            string[] databaseInfo = list.ToArray();

            // Setup the SSH connection
            Tuple<SshClient, uint> sshConnection = ConnectSsh(databaseInfo[0], databaseInfo[1], databaseInfo[2]);
            SshConnection = sshConnection.Item1;
            SshPort = sshConnection.Item2;
        }
        #endregion
    }
}