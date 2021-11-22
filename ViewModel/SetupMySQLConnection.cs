using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;

namespace ViewModel
{
    public class SetupMySQLConnection
    {
        public MySqlConnection Connection { get;  }

        private string server = "145.44.233.153";
		private string sshUserName = "student";
		private string sshPassword = "Spidb1@";
		private string databaseUserName = "sa";
		private string databasePassword = "Spidb1@!#";
		private int databasePort = 1433;

		public SetupMySQLConnection()
        {
            Tuple<SshClient, uint> sshConnection = SetupSSHConnection.ConnectSsh(server, sshUserName, sshPassword, databasePort: databasePort);

			using (sshConnection.Item1)
			{
				MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();
				//{
				//	Server = "127.0.0.1",
				//	Port = sshConnection.Item2,
				//	UserID = databaseUserName,
				//	Password = databasePassword
				//};

				csb.ConnectionString = $"server=127.0.0.1;port={sshConnection.Item2};user id={databaseUserName};password={databasePassword};connection timeout=60";

				using MySqlConnection Connection = new MySqlConnection(csb.ConnectionString);
				Connection.Open();
			}
		}
	}
}
