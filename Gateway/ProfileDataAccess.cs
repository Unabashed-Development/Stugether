using Dapper;
using Model;
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


        public static Profile GetProfile(Account account)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            string studentData = connection.QuerySingle<string>("SELECT * FROM Profile WHERE UserID = @userID", account);
            return null;
        }

        public static QAData GetQAData(Account account)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            //string studentData = connection.QuerySingle<string>("SELECT * FROM Student");
            return null;
        }

        public static InterestsData GetInterestsData(Account account)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            //string studentData = connection.QuerySingle<string>("SELECT * FROM Student");
            return null;
        }
        public static MoralsData GetMoralsData(Account account)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            //string studentData = connection.QuerySingle<string>("SELECT * FROM Student");
            return null;
        }


    }
}
