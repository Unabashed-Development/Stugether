using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Model;

namespace Gateway
{
    public static class SearchStudentsDataAccess
    {
        #region Methods


        /// <summary>
        /// Search for a list of students that have the same relation type 
        /// </summary>
        /// <param name="student">Student that is logged in</param>
        /// <returns>A list of students that have the same relation type </returns>
        public static List<Student> GetStudentsFromSearchRelationType(Student student)
        {
            var p1 = 0;
            var p2 = 0;
            var p3 = 0;
            var p4 = 0;
            //TODO make the relationships check better
            if (student.Relationships.Contains(Relationships.Business))
            {
                p1 = 2;
            }
            if (student.Relationships.Contains(Relationships.Friends))
            {
                p2 = 4;
            }
            if (student.Relationships.Contains(Relationships.Love))
            {
                p3 = 1;
            }
            if (student.Relationships.Contains(Relationships.Study))
            {
                p4 = 3;
            }

            IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));

            //var result = connection.Query<Student>("SELECT * FROM Student JOIN RelationshipPreference RP on Student.UserID = RP.UserID WHERE RP.RelationshipTypeID = 3;").ToList();
            var result = connection.Query<Student>($"exec GetStudentsWithSameRelationType {p1},{p2},{p3},{p4}").ToList();
            
            return new List<Student>(result);
        }

        #endregion



    }
}
