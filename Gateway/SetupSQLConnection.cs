using System.Data.SqlClient;
using Renci.SshNet;
using System;
using System.IO;

namespace Gateway
{
    public class SetupSQLConnection
    {
        public SqlConnection Connection { get; set; }

		private readonly string[] databaseInfo = System.IO.File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"NoPush\DatabaseInfo.txt"));

		public SetupSQLConnection()
        {
            Tuple<SshClient, uint> sshConnection = SetupSSHConnection.ConnectSsh(databaseInfo[0], databaseInfo[1], databaseInfo[2], databasePort: Int32.Parse(databaseInfo[3]));

			using (sshConnection.Item1)
			{
				SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder()
                {
                    DataSource = $"127.0.0.1,{sshConnection.Item2}",
					UserID = databaseInfo[4],
					Password = databaseInfo[5],
					InitialCatalog = databaseInfo[6]
				};

                Connection = new SqlConnection(csb.ConnectionString);
				Connection.Open();
			}
		}
	}
}
