using Dapper;
using Model;
using System.Collections.Generic;
using System.Data;

namespace Gateway
{
    public class ProfileDataAccess
    {

        public static List<string> LoadAllInterests()
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            List<string> result = (List<string>)connection.Query<string>("SELECT InterestName FROM InterestType");
            return result;
        }

        public static Profile LoadProfile(Account account)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            Profile studentData = (Profile)connection.QuerySingle<Profile>("SELECT * FROM Profile WHERE UserID = 3");


            return null;
        }

        public static QAData LoadQAData(Account account)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            //string studentData = connection.QuerySingle<string>("SELECT * FROM Student");
            return null;
        }

        public static InterestsData LoadInterestsData(Account account)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            //string studentData = connection.QuerySingle<string>("SELECT * FROM Student");
            return null;
        }
        public static MoralsData LoadMoralsData(Account account)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            //string studentData = connection.QuerySingle<string>("SELECT * FROM Student");
            return null;
        }


    }
}
