using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{

    public class Media
    {

        private List<Picture> Pictures { get; set; }
        private Student Student;
        
        public Media(Student student)
        {
            Student = student;
        }

    }
}
