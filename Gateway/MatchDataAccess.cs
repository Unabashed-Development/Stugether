using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gateway.Helpers;

namespace Gateway
{
    public enum MatchOrLike
    {
        Matched,
        Liked
    }

    public static class MatchDataAccess
    {
        /// <summary>
        /// Gets all the matches from a specific user.
        /// </summary>
        /// <param name="userID">The user ID to get the matches from.</param>
        /// <param name="searchType">The type that needs to be searched for. Can be either "Matched" or "Liked".</param>
        /// <returns>A list of matches in the form of user IDs.</returns>
        public static List<int> GetAllMatchesFromUser(int userID, MatchOrLike searchType)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                List<int> matches = new List<int>();
                matches.AddRange(connection.Query<int>($"SELECT UserID2 FROM Matches WHERE UserID = {userID} AND {searchType} = 1").ToList());
                matches.AddRange(connection.Query<int>($"SELECT UserID FROM Matches WHERE UserID2 = {userID} AND {searchType} = 1").ToList());
                return matches;
            }
        }

        /// <summary>
        /// Removes a match from a specific user.
        /// </summary>
        /// <param name="userID">The user ID to remove the match from.</param>
        /// <param name="matchedUserID">The user ID of the user that needs to be removed from the match.</param>
        public static void RemoveMatchFromUser(int userID, int matchedUserID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute(
                    $"DELETE FROM Matches " +
                    $"WHERE UserID = {userID} AND UserID2 = {matchedUserID} " +
                    $"OR UserID = {matchedUserID} AND UserID2 = {userID}");
            }
        }

        /// <summary>
        /// Adds a like from an user to another user by creating an entry in the database and setting Liked to 1.
        /// </summary>
        /// <param name="userID">The user ID that liked the other user.</param>
        /// <param name="likedUserID">The user ID the user liked.</param>
        /// <param name="relationshipTypeID">The type of relationship the user ID liked for.</param>
        public static void AddLikeToUserIDs(int userID, int likedUserID, int relationshipTypeID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute($"INSERT INTO Matches VALUES({userID}, {likedUserID}, {relationshipTypeID}, 0, 1)");
            }
        }

        /// <summary>
        /// Matches two user IDs in the database by setting Matched to 1.
        /// </summary>
        /// <param name="userID">User ID of party 1.</param>
        /// <param name="matchedUserID">User ID of party 2.</param>
        public static void SetMatchToUserIDs(int userID, int matchedUserID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute(
                    $"UPDATE Matches " +
                    $"SET Matched = 1 " +
                    $"WHERE UserID = {userID} AND UserID2 = {matchedUserID} " +
                    $"OR UserID = {matchedUserID} AND UserID2 = {userID}");
            }
        }

        /// <summary>
        /// Checks if likedUserID has liked userID.
        /// </summary>
        /// <param name="userID">The user ID of the one that needs to check if the other user liked them.</param>
        /// <param name="likedUserID">The user ID that may or may not have liked the user ID.</param>
        /// <returns>True if the user has liked them and false if not.</returns>
        public static bool CheckIfUserLiked(int userID, int likedUserID)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
                {
                    return connection.QuerySingle<bool>(
                        $"SELECT Liked FROM Matches " +
                        $"WHERE UserID = {likedUserID} AND UserID2 = {userID}");
                }
            }
            catch (InvalidOperationException) // If the key could not be found, always return false, because there was no like in the first place
            {
                return false;
            }
        }
    }
}