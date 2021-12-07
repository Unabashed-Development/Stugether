using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Gateway
{
    public class ProfileDataAccess
    {

        public static List<string> GetAllInterests()
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            return (List<string>)connection.Query<string>("SELECT HobbyName FROM HobbyType");
        }

    }
}
