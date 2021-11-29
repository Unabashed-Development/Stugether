using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Profile
    {

        public QAData QAData { get; set; }
        public MoralsData MoralsData { get; set; }
        public InterestsData InterestsData { get; set; }
        public string Description { get; set; }
        public Media Media { get; set; }
        public MatchList MatchList { get; set; }
        public BlockList BlockList { get; set; }

        public Profile(Student student)
        {
            QAData = new QAData(student);
            MoralsData = new MoralsData(student);
            InterestsData = new InterestsData(student);
            Media = new Media(student);
            MatchList = new MatchList(student);
            BlockList = new BlockList(student);
            Description = "";
        }

    }
}
