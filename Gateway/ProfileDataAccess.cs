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
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sql = "SELECT * FROM InterestType";
                List<Interest> result = (List<Interest>)connection.Query<Interest>(sql);
                return result;
            } catch(Exception) 
            {
                return null;
            }
        }

        public static InterestsData LoadInterestsData(int id)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sql = $"SELECT it.InterestID, it.InterestName, it.CategoryID FROM Interests i JOIN InterestType it ON it.InterestID = i.InterestID WHERE UserID = {id};";
                List<Interest> result = (List<Interest>)connection.Query<Interest>(sql);
                return new InterestsData(id, result);
            }
            catch (Exception) 
            {
                return null;
            }
        }

        public static Profile LoadProfile(int id)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sql = $"SELECT * FROM Profile WHERE UserID = {id};";
                Profile studentData = connection.QuerySingle<Profile>(sql);
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
            } catch (Exception) 
            {
                return null;
            }
        }

        public static School LoadSchool(int id)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sql = $"SELECT * FROM School WHERE UserID = {id};";
                School schoolData = connection.QuerySingle<School>(sql);
                return schoolData;
            }
            catch (Exception)
            {
                return null;
            }
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

        public static bool UpdateInterestsData(InterestsData interestsData)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));

                //delete all existing interests
                string sqlDelete = "DELETE FROM Interests WHERE UserID = @UserID";
                if (connection.Execute(sqlDelete, interestsData) == 0)
                {
                    return false;
                }

                //insert the new interests
                StringBuilder sb = new StringBuilder();
                List<Interest> interests = interestsData.Interests;
                if (interests.Count == 0)
                {
                    return true;
                }

                //creates a string with all the values like: (UserID, InterestID), (UserId, InterestID)
                interests.ForEach(interest =>
                {
                    _ = sb.Append("(");
                    _ = sb.Append(interestsData.UserID);
                    _ = sb.Append(",");
                    _ = sb.Append(interest.InterestID);
                    _ = sb.Append(")");
                    _ = sb.Append(",");
                });

                //remove the final , as its not necesarry
                string values = sb.ToString().TrimEnd(',');

                //insert into the db
                string sqlInsert = $"Insert INTO Interests VALUES {values};";
                return connection.Execute(sqlInsert) > 0;
            }
            catch (Exception) 
            {
                return false;
            }
        }

        public static bool UpdateProfile(Profile profile)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sqlUpdate = "UPDATE Profile SET Description = @Description, FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, City = @City, Sex = @Sex WHERE UserID = @UserID";
                string sqlInsert = "INSERT INTO Profile(UserID, Description, FirstName, LastName, DateOfBirth, City, Sex) VALUES (@UserID, @Description, @FirstName, @LastName, @DateOfBirth, @City, @Sex);";
                return (connection.Execute(sqlUpdate, profile) > 0) || connection.Execute(sqlInsert, profile) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UpdateSchool(School school)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sqlUpdate = "UPDATE School SET SchoolName = @SchoolName, SchoolCity = @SchoolStudy, Study = @Study WHERE UserID = @UserID;";
                string sqlInsert = "INSERT INTO School(UserID, SchoolName, SchoolCity, Study) VALUES (@UserID, @SchoolName, @SchoolCity, @Study);";
                return (connection.Execute(sqlUpdate, school) > 0) || connection.Execute(sqlInsert, school) > 0;
            }
            catch (Exception) 
            {
                return false;
            }
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
