using System;
using Dapper;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace Gateway
{
    /// <summary>
    /// Class that helps to load data for profiles from the database
    /// </summary>
    public class ProfileDataAccess
    {

        #region methods
        /// <summary>
        /// Loads all existing interests from the database
        /// </summary>
        /// <returns>List of Interest obj</returns>
        public static List<Interest> LoadAllInterests()
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sql = "SELECT * FROM InterestType";
                List<Interest> result = (List<Interest>)connection.Query<Interest>(sql);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Loads all the Interests that's linked to the profile with userid id
        /// </summary>
        /// <param name="id">id of the user</param>
        /// <returns>InterestsData</returns>
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

        /// <summary>
        /// Loads the profile with userid id, the profile loads here the basic data from profile, school, interestsData and usermedia
        /// </summary>
        /// <param name="id">userid of the profile</param>
        /// <returns>Profile</returns>
        public static Profile LoadProfile(int id)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sql = $"SELECT * FROM Profile WHERE UserID = {id};";

                Profile studentData = connection.QuerySingle<Profile>(sql);
                if (studentData == null)
                {
                    return null;
                }

                School school = LoadSchool(id);
                studentData.School = school;
                InterestsData interestData = LoadInterestsData(id);
                studentData.InterestsData = interestData;
                //QAData qaData = LoadQAData(id);
                //studentData.QAData = qaData;
                MoralsData moralsData = LoadMoralsData(id);
                studentData.MoralsData = moralsData;
                studentData.UserMedia = new System.Collections.ObjectModel.ObservableCollection<Uri>(MediaDataAccess.GetUserMediaUris(id));
                studentData.FirstUserMedia = studentData.UserMedia?.FirstOrDefault();
                studentData.MatchRelationType = LoadRelationshipTypeMatch(studentData.UserID);
                return studentData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Loads the school object from the database
        /// </summary>
        /// <param name="id">userid of the profile</param>
        /// <returns>School</returns>
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

        /// <summary>
        /// Loads the moralasdata object from the database
        /// </summary>
        /// <param name="id">userid of the profile</param>
        /// <returns>School</returns>
        public static MoralsData LoadMoralsData(int id)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sql = $"SELECT m.moralID, mt.moralName, m.percentage FROM Morals m JOIN MoralType mt ON mt.moralID = m.moralID WHERE UserID = {id};";
                List<Moral> result = (List<Moral>)connection.Query<Moral>(sql);
                result.OrderBy(moral => moral.MoralID);
                return new MoralsData(id, result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// On creating an account, an empty profile has to be inserted into the database
        /// </summary>
        /// <param name="id">userid of the new profile</param>
        public static void CreateEmptyProfile(int id)
        {
            Profile newProfile = new Profile()
            {
                UserID = id,
                DateOfBirth = null,
                School = new School(id, null, null, null),
                MoralsData = new MoralsData(id, new List<Moral>() 
                { 
                    new Moral(1, "Intelligentie", 50 ), 
                    new Moral(2, "Fysieke activiteiten", 50),
                    new Moral(3, "Uitgaansleven", 50),
                    new Moral(4, "Natuur", 50),
                    new Moral(5, "Politiek", 50),
                    new Moral(6, "Werk", 50),
                    new Moral(7, "Klimaat", 50),
                }),
                Sex = true
            };
            _ = UpdateProfile(newProfile);
            _ = UpdateSchool(newProfile.School);
            _ = UpdateMoralsData(newProfile.MoralsData);
        }

        /// <summary>
        /// Updates or inserts InterestsData in the database
        /// </summary>
        /// <param name="interestsData"></param>
        /// <returns>bool, false if failed to submit to the db</returns>
        public static bool UpdateInterestsData(InterestsData interestsData)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));

                //delete all existing interests
                string sqlDelete = "DELETE FROM Interests WHERE UserID = @UserID";
                connection.Execute(sqlDelete, interestsData);

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

        /// <summary>
        /// Updates or loads the profile in the database
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>bool, false if failed to submit to the db</returns>
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

        /// <summary>
        /// Updates or loads the school in the database
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public static bool UpdateSchool(School school)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sqlUpdate = "UPDATE School SET SchoolName = @SchoolName, SchoolCity = @SchoolCity, Study = @Study WHERE UserID = @UserID;";
                string sqlInsert = "INSERT INTO School(UserID, SchoolName, SchoolCity, Study) VALUES (@UserID, @SchoolName, @SchoolCity, @Study);";
                return (connection.Execute(sqlUpdate, school) > 0) || connection.Execute(sqlInsert, school) > 0;
            }
            catch (Exception e)
            {
                _ = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Updates or loads the moralsdata in the database
        /// </summary>
        /// <param name="MoralsData"></param>
        /// <returns></returns>
        public static bool UpdateMoralsData(MoralsData moralsData)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            
            //delete current values in morals
            string sqlDelete = "DELETE FROM Morals WHERE UserID = @UserID;";
            connection.Execute(sqlDelete, moralsData);

            //insert new values in morals
            StringBuilder sb = new StringBuilder();
            List<Moral> morals = moralsData.Morals;
            if (morals.Count == 0)
            {
                return true;
            }

            //creates a string with all the values like: (UserID, MoralID, MoralPercentage), (UserID, MoralID, MoralPercentage)
            morals.ForEach(moral =>
            {
                _ = sb.Append("(");
                _ = sb.Append(moralsData.UserID);
                _ = sb.Append(",");
                _ = sb.Append(moral.MoralID);
                _ = sb.Append(",");
                _ = sb.Append(moral.Percentage);
                _ = sb.Append(")");
                _ = sb.Append(",");
            });

            //remove the final , as its not necesarry
            string values = sb.ToString().TrimEnd(',');

            string sqlInsert = $"INSERT INTO Morals VALUES {values};";
            return (connection.Execute(sqlInsert, moralsData) > 0);
        }

        //reserved for sprint 3
        public static QAData LoadQAData(int id)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            //string studentData = connection.QuerySingle<string>("SELECT * FROM Student");
            return null;
        }

        /// <summary>
        /// Loads the RelationshipTypeMatch from the database
        /// </summary>
        /// <param name="id">userid of the profile</param>
        /// <returns>School</returns>
        public static string LoadRelationshipTypeMatch(int id)
        {
            if (id != Account.UserID.Value){
                try
                {
                    using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                    string sql = $"SELECT RelationshipName FROM RelationshipType WHERE RelationshipTypeID = (SELECT RelationshipTypeID FROM Matches WHERE UserID = {Account.UserID.Value} AND UserID2 = {id} OR UserID = {id} AND UserID2 = {Account.UserID.Value});";
                    string RelationshipTypeMatch = connection.QuerySingle<string>(sql);
                    return RelationshipTypeMatch;
                }
                catch (Exception)
                {
                    return null;                    
                }
                
            }
            return "Me";            
        }

        #endregion
    }
}
