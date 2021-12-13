using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Holds all the interests of the user with UserID
    /// </summary>
    public class InterestsData
    {

        #region properties
        public int UserID { get; set; }
        public List<Interest> Interests { get; set; }
        #endregion

        #region constructors
        public InterestsData(int userID, List<Interest> interest)
        {
            UserID = userID;
            Interests = interest;
        }
        #endregion

    }
}
