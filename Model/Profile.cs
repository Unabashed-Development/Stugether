using System;
using System.Collections.Generic;

namespace Model
{
    public class Profile
    {

        public int ID { get; set; }
        public QAData QAData { get; set; }
        public MoralsData MoralsData { get; set; }
        public InterestsData InterestsData { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth 
        {
            get => DateOfBirth;
            set
            {
                DateOfBirth = value;
                Age = CalculateAge(value).ToString() + " Jaar";
            } 
        }
        public string Age { get; set; }
        public School School { get; set; }
        public HashSet<int> Relationships { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }

        public Profile(int id, string firstName, string lastName, string age, bool sex, School school, HashSet<int> relationships, QAData qaData, MoralsData moralsData, InterestsData interestsData, string description, string city, DateTime dateOfBirth)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Sex = sex;
            School = school;
            Relationships = relationships;
            QAData = qaData;
            MoralsData = moralsData;
            InterestsData = interestsData;
            Description = description;
            City = city;
            DateOfBirth = dateOfBirth;
        }

        private static int CalculateAge(DateTime birthDay)
        {
            int years = DateTime.Now.Year - birthDay.Year;
            if ((birthDay.Month > DateTime.Now.Month) || (birthDay.Month == DateTime.Now.Month && birthDay.Day > DateTime.Now.Day))
            {
                years--;
            }
            return years;
        }

    }
}
