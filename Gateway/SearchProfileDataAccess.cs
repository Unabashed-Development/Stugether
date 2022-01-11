using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Model;

namespace Gateway
{
    public static class SearchProfileDataAccess
    {

        
        #region Methods

        #region ReturnListMethod

        /// <summary>
        /// executes the sql query and te turned a list
        /// </summary>
        /// <param name="sqlQuery">the sql query that you want to execute must be a stored presedure</param>
        /// <param name="id">The user_id</param>
        /// <returns>Return a list of profiles that matcht the query result</returns>
        private static List<Profile> ReturnProfileList(string sqlQuery, int id)
        {
            List<Profile> pfList = new List<Profile>();
            try
            {
                IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                pfList = connection.Query<Profile>(sqlQuery).ToList();
                pfList.RemoveAll(i => i.UserID == id);
                var newresult = blockedUser(pfList, id, connection);
                return newresult;
            }
            catch (Exception e)
            {
                return null;
            }


        }

        /// <summary>
        /// removes blocked users and users where an relation is already acquired
        /// </summary>
        /// <param name="pfList"></param>
        /// <param name="id"></param>
        /// <param name="conn"></param>
        /// <returns>a list that is filterd on people that are not blocked or already linked</returns>
        private static List<Profile> blockedUser(List<Profile> pfList, int id ,IDbConnection conn)
        {
            List<int> blockedList = new List<int>();
            blockedList = conn.Query<int>($"select BlockedUserID from BlockList WHERE userid = {id}").ToList();
            var result1 = conn.Query<int>($"select UserID2 from Matches WHERE  UserID = {id} and Liked = 1").ToList();
            var result2 =conn.Query<int>($"select UserID from Matches WHERE  UserID2 = {id} and Liked = 1").ToList();

            var acquiredMatchAndBlocked = result1.Concat(result2).Concat(blockedList).ToList();

            foreach (var amb in acquiredMatchAndBlocked)
            {
                pfList.RemoveAll(i => i.UserID == amb);
            }
            return pfList;
        }

        #endregion



        /// <summary>
        /// Search in the databse for all profiles that have the same relationtype
        /// </summary>
        /// <param name="user_id">The user_id</param>
        /// <returns>Return a list of profiles that match the loggedin user relationtypes</returns>
        public static List<Profile> GetProfileBasedOnRelationType(int user_id)
        {
            string query = $"exec GetProfileBasedOnRelationType {user_id}";
            return ReturnProfileList(query,user_id);
        }




        /// <summary>
        /// Search in the databse for all profiles that have the same relationtype
        /// </summary>
        /// <param name="id">The user_id</param>
        /// <returns>Return a list of profiles that match the loggedin user relationtypes</returns>
        public static List<Profile> GetProfileBasedOnInterest(int id)
        {
            string query = $"exec GetProfileBasedOnInterests {id}";
            return ReturnProfileList(query,id);
        }
        #endregion
    }
}
