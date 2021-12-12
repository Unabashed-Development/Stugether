using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gateway
{
    public static class MatchDataAccess
    {
        /// <summary>
        /// Gets all the matches from a specific user.
        /// </summary>
        /// <param name="userID">The user ID to get the matches from.</param>
        /// <returns>A list of matches in the form of user IDs.</returns>
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

        /// <summary>
        /// Removes a match from a specific user.
        /// </summary>
        /// <param name="userID">The user ID to remove the match from.</param>
        /// <param name="toRemoveMatchUserID">The user ID of the user that needs to be removed from the match.</param>
        public static void RemoveMatchFromUser(int userID, int toRemoveMatchUserID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute(
                    $"DELETE FROM Matches " +
                    $"WHERE UserID = {userID} AND UserID2 = {toRemoveMatchUserID} " +
                    $"OR UserID = {toRemoveMatchUserID} AND UserID2 = {userID}");
            }
        }
    }
}
