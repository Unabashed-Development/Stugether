using System.Configuration;

namespace Gateway
{
    public static class FiddleHelper
    {
        /// <summary>
        /// Assigns the right connection name to the connection string using the randomized port.
        /// </summary>
        /// <param name="name">The name of the connection.</param>
        /// <returns>A string of the ConfigurationManager.</returns>
        public static string GetConnectionStringSql(string name)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            int indexOfServer = connectionString.IndexOf(';');
            return connectionString.Insert(indexOfServer, ',' + SSHService.SshPort.ToString());
        }
    }
}
