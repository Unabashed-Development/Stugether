using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class School
    {

        public string Name { get; set; }
        public string City { get; set; }
        public string Study { get; set; }

        public School(string name, string city, string study)
        {
            Name = name;
            City = city;
            Study = study;
        }

    }
}
