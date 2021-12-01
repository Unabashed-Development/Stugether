using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /*This class contains the list of all blocked students for the current user*/
    public class BlockList
    {

        public List<Student> BlockedStudents { get; set; }
        private Student Student;
        public BlockList(Student student)
        {
            Student = student;
            BlockedStudents = new List<Student>();
            //update list according to database
        }

        /*Adds arg student to blocked list*/
        public void AddBlock(Student student)
        {
            BlockedStudents.Add(student);
            //database update
        }

        /*removes arg student from blocked list*/
        public void RemoveBlock(Student student)
        {
            BlockedStudents.Remove(student);
            //database update
        }

        /*returns if arg student is in the blocked list*/
        public bool IsBlocked(Student student)
        {
            return BlockedStudents.Contains(student);
        }

    }
}
