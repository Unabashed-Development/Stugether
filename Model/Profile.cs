using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Profile
    {

        private Student Student { get; set; }
        private QAData QAData { get; set; }
        private MoralsData MoralsData { get; set; }
        private InterestsData InterestsData { get; set; }
        private string Description { get; set; }
        private Media Media { get; set; }
        private MatchList MatchList { get; set; }
        private BlockList BlockList { get; set; }

        public Profile(Student student)
        {
            Student = student;
            QAData = new QAData(student);
            MoralsData = new MoralsData(student);
            InterestsData = new InterestsData(student);
            Media = new Media(student);
            MatchList = new MatchList(student);
            BlockList = new BlockList(student);
            Description = ""; //get description from db

        }

    }
}
