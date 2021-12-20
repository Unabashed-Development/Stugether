using System.Collections.Generic;

namespace Model
{
    /*This class contains the list of all blocked students for the current user*/
    /*Reserved for sprint 3*/
    public class BlockList
    {

        #region properties
        public List<int> BlockedStudents { get; set; }
        #endregion

        #region constructors
        public BlockList()
        {
            BlockedStudents = new List<int>();
            //update list according to database
        }
        #endregion

        #region methods
        /*Adds arg student to blocked list*/
        public void AddBlock(int student)
        {
            BlockedStudents.Add(student);
            //database update
        }

        /*removes arg student from blocked list*/
        public void RemoveBlock(int student)
        {
            BlockedStudents.Remove(student);
            //database update
        }

        /*returns if arg student is in the blocked list*/
        public bool IsBlocked(int student)
        {
            return BlockedStudents.Contains(student);
        }
        #endregion
    }
}
