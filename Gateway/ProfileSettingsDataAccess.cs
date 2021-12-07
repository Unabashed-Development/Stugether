using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Gateway
{
    public class ProfileSettingsDataAccess
    {

        public static List<string> GetAllInterests()
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            return (List<string>) connection.Query<string>("SELECT HobbyName FROM HobbyType");
        }

    }
}
