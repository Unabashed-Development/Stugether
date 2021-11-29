using System.Configuration;

namespace Gateway
{
    public static class FiddleHelper
    {
        /// <summary>
        /// Assigns the right connection name to the connection string.
        /// </summary>
        /// <param name="name">The name of the connection.</param>
        /// <returns>A string of the ConfigurationManager.</returns>
        public static string GetConnectionStringSql(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
