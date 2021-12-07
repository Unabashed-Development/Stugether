using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Gateway
{
    public static class MediaDataAccess
    {
        public static IEnumerable<string> GetUserMedia(int userID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                return connection.Query<string>("SELECT Path FROM UserMedia WHERE UserID = @uid", new { uid = userID });
            }
        }

    }
}
