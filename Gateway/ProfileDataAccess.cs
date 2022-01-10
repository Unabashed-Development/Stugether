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
                QAData qaData = LoadQAData(id);
                studentData.QAData = qaData;
                MoralsData moralsData = LoadMoralsData(id);
                studentData.MoralsData = moralsData;
                studentData.UserMedia = new System.Collections.ObjectModel.ObservableCollection<Uri>(MediaDataAccess.GetUserMediaUris(id));
                studentData.FirstUserMedia = studentData.UserMedia?.FirstOrDefault();
                return studentData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// updates the Qa data in the database
        /// </summary>
        /// <param name="id">userid of the profile</param>
        /// <returns>bool</returns>
        public static bool UpdateQaData(QAData qAData)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));

            //delete current values in morals
            string sqlDelete = "DELETE FROM Qa WHERE UserID = @UserID;";
            connection.Execute(sqlDelete, qAData);

            //insert new values in morals
            StringBuilder sb = new StringBuilder();
            List<QA> qaList = qAData.QAList;
            if (qaList.Count == 0)
            {
                return true;
            }

            //creates a string with all the values like: (UserID, MoralID, MoralPercentage), (UserID, MoralID, MoralPercentage)
            qaList.ForEach(qa =>
            {
                _ = sb.Append("(");
                _ = sb.Append(qa.QaID);
                _ = sb.Append(",");
                _ = sb.Append(qAData.UserID);
                _ = sb.Append(",");
                _ = sb.Append($"N'{qa.QaAnswer}'");
                _ = sb.Append(")");
                _ = sb.Append(",");
            });

            //remove the final , as its not necesarry
            string values = sb.ToString().TrimEnd(',');

            string sqlInsert = $"INSERT INTO Qa VALUES {values};";
            return (connection.Execute(sqlInsert) > 0);
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
                MoralsData moralsData = new MoralsData(id, result);
                return moralsData;
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
            morals.OrderBy(m => m.MoralID);

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
            return (connection.Execute(sqlInsert) > 0);
        }

        /// <summary>
        /// Loads all the qa questions from the database
        /// </summary>
        /// <returns>QAData</returns>
        public static QAData LoadAllQAData()
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            string sql = "SELECT QaID, QaQuestion FROM QaType";
            List<QA> result = (List<QA>)connection.Query<QA>(sql);
            result.OrderBy(qa => qa.QaID);
            return new QAData(-1, result);
        }

        /// <summary>
        /// Loads the QAData from user (answered questions) from the database
        /// </summary>
        /// <param name="id">userid of the profile</param>
        /// <returns>QAData</returns>
        public static QAData LoadQAData(int id)
        {
            try
            {
                using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                string sql = $"SELECT Qa.QaID, QT.QaQuestion, Qa.QaAnswer FROM Qa JOIN QaType QT on Qa.QaID = QT.QaID WHERE Qa.UserID = {id}";
                List<QA> result = (List<QA>)connection.Query<QA>(sql);
                result.OrderBy(qa => qa.QaID);
                return new QAData(id, result);
            }
            catch(Exception e)
            {
                string a = e.Message;
            }
            return null;

        }

        #endregion
    }
}
