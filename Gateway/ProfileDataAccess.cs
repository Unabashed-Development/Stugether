using Dapper;
using Model;
using System.Collections.Generic;
using System.Data;

namespace Gateway
{
    public class ProfileDataAccess
    {

        public static List<Interest> LoadAllInterests()
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            List<Interest> result = (List<Interest>)connection.Query<Interest>("SELECT * FROM InterestType"); //change to interests
            return result;
        }

        public static InterestsData LoadInterestsData(int id)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            List<Interest> result = (List<Interest>)connection.Query<Interest>($"SELECT it.InterestID, it.InterestName, it.CategoryID FROM Interests i JOIN InterestType it ON it.InterestID = i.InterestID WHERE UserID = {id};");
            return new InterestsData(id, result);
        }

        public static Profile LoadProfile(int id)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            Profile studentData = connection.QuerySingle<Profile>($"SELECT * FROM Profile WHERE UserID = {id};");

            School school = LoadSchool(id);
            studentData.School = school;
            InterestsData interestData = LoadInterestsData(id);
            studentData.InterestsData = interestData;
/*            QAData qaData = LoadQAData(id);
            studentData.QAData = qaData;
            MoralsData moralsData = LoadMoralsData(id);
            studentData.MoralsData = moralsData;*/
            return studentData;
        }

        public static School LoadSchool(int id)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            School schoolData = connection.QuerySingle<School>($"SELECT * FROM School WHERE UserID = {id};");
            return schoolData;
        }

        public static QAData LoadQAData(int id)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            //string studentData = connection.QuerySingle<string>("SELECT * FROM Student");
            return null;
        }

        public static MoralsData LoadMoralsData(int id)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            //string studentData = connection.QuerySingle<string>("SELECT * FROM Student");
            return null;
        }


    }
}
