using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{

    public enum Relationships { Friends, Business, Love, Study };
    internal enum DataKeys { Education, Address, DateOfBirth, School, Relationships}
    public class Student
    {

        //public Account Account { get; set; }
        public int ID { get; set; }
        public string Email { get; set; }
        public string Eduction { get; set; }
        public string Address { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public School School { get; set; }
        public HashSet<Relationships> Relationships { get; set; }
        public Profile Profile { get; set; }

        public string Name { get; set; }

        public Student(string email)
        {
            //Account = account;
            Email = email;
            Eduction = (string)LoadObject(DataKeys.Education);
            Address = (string)LoadObject(DataKeys.Address);
            DateOfBirth = (DateTime)LoadObject(DataKeys.DateOfBirth);
            School = (School)LoadObject(DataKeys.School);
            Relationships = (HashSet<Relationships>)LoadObject(DataKeys.Relationships);
            Profile = new Profile(this);
        }

        public Student()
        {

        }

        private object LoadObject(DataKeys data)
        {

            //this method will connect to the database and load the correct data for the object
            switch (data)
            {
                case DataKeys.Education:
                    break;
                case DataKeys.Address:
                    break;
                case DataKeys.DateOfBirth:
                    break;
                case DataKeys.School:
                    break;
                case DataKeys.Relationships:
                    break;
                default:
                    break;
            }

            return null;
        }


    }
}
