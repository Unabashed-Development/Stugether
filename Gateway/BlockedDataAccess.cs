using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Gateway
{
    public enum BlockReason
    {
        Disliked,
        Unmatched,
        Blocked
    }

    public static class BlockedDataAccess
    {

        /// <summary>
        /// Gives a list of BlockedUserIDs that are blocked by the User. This list is based on the reason they are blocked.
        /// </summary>
        /// <param name="userID">User that blocked the users on the list.</param>
        /// <param name="blockReason">Reason the users on the list are blocked. Examples: 'Disliked', 'Unmatched', 'Blocked'.</param>
        /// <returns></returns>
        public static List<int> GetAllBlockedIDsFromUser(int userID, BlockReason blockReason)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                List<int> Blocked = new List<int>();
                Blocked.AddRange(connection.Query<int>($"SELECT BlockedUserID FROM BlockList WHERE UserID = {userID} AND BlockReasonID = (Select BlockReasonID FROM BlockReason WHERE BlockReasonDescription = '{blockReason}')").ToList());
                return Blocked;
            }
        }

        /// <summary>
        /// Block a specific user for another user, with a given reason.
        /// </summary>
        /// <param name="userID">UserID of the person blocking.</param>
        /// <param name="blockedUserID">UserID of the person being blocked.</param>
        /// <param name="blockReason">Reason for the blocked user to be blocked. Examples: 'Disliked', 'Unmatched', 'Blocked'.</param>
        public static void BlockUserID(int userID, int blockedUserID, BlockReason blockReason)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute($"INSERT INTO BlockList VALUES ({userID}, {blockedUserID}, (Select BlockReasonID FROM BlockReason WHERE BlockReasonDescription = '{blockReason}'))");
            }
        }

        /// <summary>
        /// Unblock a specific user for another user. 
        /// </summary>
        /// <param name="userID">UserID of the person unblocking.</param>
        /// <param name="blockedUserID">UserID of the person being unblocked.</param>
        public static void UnblockUserID (int userID, int blockedUserID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute($"DELETE FROM BlockList WHERE UserID = {userID} AND BlockedUserID = {blockedUserID}");
            }
        }

        /// <summary>
        /// Unblock all users that were blocked for a specific reason by another user.
        /// </summary>
        /// <param name="userID">UserID of the person that blocked them.</param>
        /// <param name="blockReason">Reason the blocked users were blocked. Examples: 'Disliked', 'Unmatched', 'Blocked'.</param>
        public static void UnblockAllUserIDsForReason (int userID, BlockReason blockReason)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute($"DELETE FROM BlockList WHERE UserID = {userID} AND BlockReasonID = (Select BlockReasonID FROM BlockReason WHERE BlockReasonDescription = '{blockReason}')");
            }
        }

    }
}
