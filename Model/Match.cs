using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Match
    {

        private Student Student1 { get; set; }
        private Student Student2 { get; set; }
        private Relationships RelationshipsType { get; set; }

        public Match(Student student1, Student student2, Relationships relationshipsType)
        {
            Student1 = student1;
            Student2 = Student2;
            RelationshipsType = relationshipsType;
        }

    }
}
