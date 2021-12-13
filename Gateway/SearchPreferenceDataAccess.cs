using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Model;

namespace Gateway
{
    public static class SearchPreferenceDataAccess
    {

        public static RelationType GetRelationType(int id)
        {
            List<int> result = new List<int>();
            RelationType rt = new RelationType();
            IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            result = connection.Query<int>($"SELECT RelationshipTypeID FROM RelationshipPreference WHERE UserID = {id};").ToList();

            foreach (var r in result)
            {
                if (r == 1)
                    rt.Love = true;
                
                if (r == 2)
                    rt.Business = true;
                
                if (r == 3)
                    rt.Friend = true;
                
                if (r == 4)
                    rt.StudyBuddy = true;
            }
            return rt;
        }

        public static void SaveRelationPreference(RelationType rt , int id)
        {
            IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            connection.Execute($"DELETE FROM RelationshipPreference WHERE UserID = {id}");
            if (rt.Love)
                connection.Execute($"Insert into RelationshipPreference values ({id},1)");
            if (rt.Business)
                connection.Execute($"Insert into RelationshipPreference values ({id},2)");
            if (rt.StudyBuddy)
                connection.Execute($"Insert into RelationshipPreference values ({id},3)");
            if (rt.Friend)
                connection.Execute($"Insert into RelationshipPreference values ({id},4)");
        }

    }
}
