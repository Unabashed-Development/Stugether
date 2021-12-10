using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Model;

namespace Gateway
{
    public static class SearchProfileDataAcces
    {
        #region Methods

        /// <summary>
        /// Search in the databse for all profiles that have the same relationtype
        /// </summary>
        /// <param name="user_id">The user_id</param>
        /// <returns>Return a list of profiles that match the loggedin user relationtypes</returns>
        public static List<Profile> GetProfileBasedOnRelationType(int user_id)
        {
            List<Profile> pfs = new List<Profile>();
            try
            {
                IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                pfs = connection.Query<Profile>($"exec GetProfileBasedOnRelationType {user_id}").ToList();
                pfs.RemoveAll(i => i.UserID == user_id);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

            return pfs;
        }


        #endregion

    }
}
