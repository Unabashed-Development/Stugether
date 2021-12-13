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
                return pfList;
            }
            catch (Exception e)
            {
                return null;
            }


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
        public static List<Profile> GetProfileBasedOnIntrest(int id)
        {
            string query = $"exec GetProfileBasedOnInterests {id}";
            return ReturnProfileList(query,id);
        }
        #endregion
    }
}
