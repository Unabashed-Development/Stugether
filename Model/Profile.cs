using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Profile class holds all the information needed to show information for the profile page
    /// </summary>
    public class Profile
    {

        #region fields
        private DateTime? _dateOfBirth;
        #endregion

        #region static properties
        public static Profile LoggedInProfile { get; set; }
        #endregion

        #region properties
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
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                Age = (value == null) ? "0 jaar" : CalculateAge(value.Value).ToString() + " jaar";
                if (_dateOfBirth != null && _dateOfBirth.Value.Day == DateTime.Now.Day && _dateOfBirth.Value.Month == DateTime.Now.Month)
                {
                    Birthday = true;
                }
            }
        }
        public bool Birthday { get; set; }
        public School School { get; set; }
        public List<Uri> UserMedia { get; set; }
        public Uri FirstUserMedia { get; set; }
        public int UnreadChatMessages { get; set; }
        public bool HasUnreadChatMessages { get => UnreadChatMessages > 0; }
        #endregion

        #region constructors
        public Profile()
        {
            UserMedia = new List<Uri>();
        }
        #endregion

        #region methods
        /// <summary>
        /// Calculates the current age from DateTime birthday param
        /// </summary>
        /// <param name="birthDay">The DateTime of the birthday of the person</param>
        /// <returns>age in int</returns>
        private int CalculateAge(DateTime birthDay)
        {
            if (birthDay == null)
            {
                return 0;
            }

            int years = DateTime.Now.Year - birthDay.Year;
            if ((birthDay.Month > DateTime.Now.Month) || (birthDay.Month == DateTime.Now.Month && birthDay.Day > DateTime.Now.Day))
            {
                years--;
            }
            return years;
        }
        #endregion
    }
}