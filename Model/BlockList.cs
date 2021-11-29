using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BlockList
    {

        private List<Student> BlockedStudents { get; set; }
        private Student Student;

        public BlockList(Student student)
        {
            Student = student;
            BlockedStudents = new List<Student>();
            //update list according to database
        }

        public void AddBlock(Student student)
        {
            BlockedStudents.Add(student);
            //database update
        }

        public void RemoveBlock(Student student)
        {
            BlockedStudents.Remove(student);
            //database update
        }

        public bool IsBlocked(Student student)
        {
            return BlockedStudents.Contains(student);
        }

    }
}
