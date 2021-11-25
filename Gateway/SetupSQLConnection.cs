using System.Data.SqlClient;
using Renci.SshNet;
using System;
using System.IO;

namespace Gateway
{
    public class SetupSQLConnection
    {
        public SqlConnection Connection { get; set; }

        /// <summary>
        /// Gets the database info, such as sensitive password data, from the remote file.
        /// </summary>
        private readonly string[] databaseInfo = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"NoPush\DatabaseInfo.txt"));

        /// <summary>
        /// Sets up an SSL connection and binds it to the Connection property.
        /// </summary>
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
            }
        }

        /// <summary>
        /// Opens the SQL connection.
        /// </summary>
        public void OpenSQLConnection() => Connection.Open();

        /// <summary>
        /// Close the SQL connection.
        /// </summary>
        public void CloseSQLConnection() => Connection.Close();
    }
}
