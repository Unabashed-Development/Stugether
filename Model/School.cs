namespace Model
{
    /// <summary>
    /// Holds the data for School that is binded to an user with userID
    /// It holds the name, city of the school and the study that user follows
    /// Reserverd for sprint 3
    /// </summary>
    public class School
    {

        #region properties
        public int UserID { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCity { get; set; }
        public string Study { get; set; }
        #endregion

        #region constuctors
        /// <summary>
        /// constructor for the school class, it's linked to the user with "userID"
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="schoolName"></param>
        /// <param name="schoolCity"></param>
        /// <param name="study"></param>
        public School(int userId, string schoolName, string schoolCity, string study)
        {
            UserID = userId;
            SchoolName = schoolName;
            SchoolCity = schoolCity;
            Study = study;
        }
        #endregion

    }
}
