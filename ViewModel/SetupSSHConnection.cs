using Renci.SshNet;
using System;
using System.Collections.Generic;

namespace ViewModel // Source: https://mysqlconnector.net/tutorials/connect-ssh/
{
    public static class SetupSSHConnection
    {
		/// <summary>
		/// Sets up a SSH connection to a remote SSH server. Needs: dotnet add package SSH.NET
		/// </summary>
		public static Tuple<SshClient, uint> ConnectSsh(string sshHostName, string sshUserName, string sshPassword = null,
		string sshKeyFile = null, string sshPassPhrase = null, int sshPort = 22, string databaseServer = "localhost", int databasePort = 3306)
		{
			// Check arguments
			if (string.IsNullOrEmpty(sshHostName))
				throw new ArgumentException($"{nameof(sshHostName)} must be specified.", nameof(sshHostName));
			if (string.IsNullOrEmpty(sshHostName))
				throw new ArgumentException($"{nameof(sshUserName)} must be specified.", nameof(sshUserName));
			if (string.IsNullOrEmpty(sshPassword) && string.IsNullOrEmpty(sshKeyFile))
				throw new ArgumentException($"One of {nameof(sshPassword)} and {nameof(sshKeyFile)} must be specified.");
			if (string.IsNullOrEmpty(databaseServer))
				throw new ArgumentException($"{nameof(databaseServer)} must be specified.", nameof(databaseServer));

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
            ForwardedPortLocal forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort);
			sshClient.AddForwardedPort(forwardedPort);
			forwardedPort.Start();

			return Tuple.Create(sshClient, forwardedPort.BoundPort);
		}
	}
}
