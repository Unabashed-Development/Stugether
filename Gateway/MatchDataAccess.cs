using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gateway
{
    public static class MatchDataAccess
    {
        public static List<int> GetAllMatchesFromUser(int userID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                List<int> matches = new List<int>();
                matches.AddRange(connection.Query<int>($"SELECT UserID2 FROM Matches WHERE UserID = {userID} AND Matched = 1").ToList());
                matches.AddRange(connection.Query<int>($"SELECT UserID FROM Matches WHERE UserID2 = {userID} AND Matched = 1").ToList());
                return matches;
            }
        }
    }
}
