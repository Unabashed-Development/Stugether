using System;
using Dapper;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

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
            //QAData qaData = LoadQAData(id);
            //studentData.QAData = qaData;
            //MoralsData moralsData = LoadMoralsData(id);
            //studentData.MoralsData = moralsData;
            studentData.UserMedia = new List<Uri>(MediaDataAccess.GetUserMediaUris(id));
            studentData.FirstUserMedia = studentData.UserMedia?.FirstOrDefault();

            return studentData;
        }

        public static void CreateEmptyProfile(int id)
        {
            Profile newProfile = new Profile()
            {
                UserID = id,
                DateOfBirth = null,
                School = new School(id, null, null, null)
            };
            UpdateProfile(newProfile);
            UpdateSchool(newProfile.School);
        }

        public static School LoadSchool(int id)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                School schoolData = connection.QuerySingle<School>($"SELECT * FROM School WHERE UserID = {id};");
                return schoolData;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public static bool UpdateInterestsData(InterestsData interestsData)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            try
            {
                //delete all existing interests
                _ = connection.Execute($"DELETE FROM Interests WHERE UserID = {interestsData.UserID}");

                //insert the new interests
                StringBuilder sb = new StringBuilder();
                List<Interest> interests = interestsData.Interests;
                if (interests.Count == 0)
                {
                    return true;
                }

                interests.ForEach(interest =>
                {
                    sb.Append("(");
                    sb.Append(interestsData.UserID);
                    sb.Append(",");
                    sb.Append(interest.InterestID);
                    sb.Append(")");
                    sb.Append(",");
                });

                string values = sb.ToString().TrimEnd(',');
                return connection.Execute($"Insert INTO Interests VALUES {values};") > 0;
            }
            catch (Exception) { }
            return false;
        }

        public static bool UpdateProfile(Profile profile)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            try
            {
                return connection.Execute($"UPDATE Profile SET Description = @Description, FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, City = @City, Sex = @Sex WHERE UserID = @UserID", profile) != 0 ||
                       connection.Execute($"INSERT INTO Profile(UserID, Description, FirstName, LastName, DateOfBirth, City, Sex) VALUES (@UserID, @Description, @FirstName, @LastName, @DateOfBirth, @City, @Sex);", profile) > 0;
            }
            catch (Exception) { }
            return false;
        }

        public static bool UpdateSchool(School school)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            try
            {
                return connection.Execute($"UPDATE School SET SchoolName = '{school.SchoolName}', SchoolCity = '{school.SchoolCity}', Study = '{school.Study}' WHERE UserID = {school.UserID};") != 0 || connection.Execute($"INSERT INTO School(UserID, SchoolName, SchoolCity, Study) VALUES ({school.UserID}, '{school.SchoolName}', '{school.SchoolCity}', '{school.Study}');") > 0;
            }
            catch (Exception) { }
            return false;
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
