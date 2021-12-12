using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Profile
    {

        public static Profile LoggedInProfile { get; set; }

        private DateTime? _dateOfBirth; // Nullable
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public bool? Sex { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public QAData QAData { get; set; }
        public MoralsData MoralsData { get; set; }
        public InterestsData InterestsData { get; set; }
        public DateTime? DateOfBirth // Nullable
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                Age = (value == null) ? "0 jaar" : CalculateAge(value.Value).ToString() + " Jaar";
            }
        }
        public School School { get; set; }
        private int CalculateAge(DateTime birthDay)
        {
            int years = DateTime.Now.Year - birthDay.Year;
            if ((birthDay.Month > DateTime.Now.Month) || (birthDay.Month == DateTime.Now.Month && birthDay.Day > DateTime.Now.Day))
            {
                years--;
            }
            return years;
        }

        public Profile()
        {
            UserMedia = new List<Uri>();
        }

        public List<Uri> UserMedia { get; set; }
        public Uri FirstUserMedia { get; set; }
    }
}
