using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Model;
using Dapper;

namespace Gateway
{
    public static class RelationShipDataAcces
    {

        #region AllHashSetItems

        private static HashSet<Relationships> _allItems;


        private static void AddAllItems()
        {
            _allItems = new HashSet<Relationships>();
            _allItems.Add(Relationships.Business);
            _allItems.Add(Relationships.Love);
            _allItems.Add(Relationships.Friends);
            _allItems.Add(Relationships.Study);
        }


        #endregion



        /// <summary>
        /// Save checked relations and removes the unchecked  
        /// </summary>
        /// <param name="student">Student that is logged in</param>
        /// <returns>return a bool that can be used to notify the user that the save has been succesfull </returns>
        public static bool SaveRelations(Student student)
        {
            AddAllItems();
            try
            {
                IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
                int user_id = 1;
                int relation_id = 0;
                var list = student.Relationships.ToList();
                foreach (var item in list)
                {
                    relation_id = (int)item;
                    connection.Execute($"exec InsertRelationShips {user_id},{relation_id}");
                }
                _allItems.ExceptWith(student.Relationships);
                var itemsList = _allItems.ToList();
                foreach (var item in itemsList)
                {
                    relation_id = (int)item;
                    connection.Execute($"exec DeleteRelationShips {user_id},{relation_id}"); 
                }



            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
