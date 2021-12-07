using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{

    public enum Relationships { Friends, Business, Love, Study };
    internal enum DataKeys { Education, City, DateOfBirth, School, Relationships}
    public class Student
    {

        //public Account Account { get; set; }
        public int ID { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public School School { get; set; }
        public HashSet<Relationships> Relationships { get; set; }
        public Profile Profile { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }

        public Student(string email)
        {
            //Account = account;
            Email = email;
            City = (string)"";
            DateOfBirth = (DateTime)DateTime.MaxValue;
            Age = CalculateAge(DateOfBirth).ToString() + " Jaar";
            School = (School)new School("", "", "");
            Relationships = (HashSet<Relationships>)new HashSet<Relationships>();

            Profile = new Profile(this);
        }

        public Student()
        {
            Profile = new Profile(this);
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
