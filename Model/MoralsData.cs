using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Holds the data for all the morals of a profile
    /// Reserverd for sprint 3
    /// </summary>
    public class MoralsData
    {

        #region properties
        public int UserID { get; set; }
        public List<Moral> Morals { get; set; }
        #endregion

        #region constuctors
        public MoralsData(int userID, List<Moral> morals)
        {
            UserID = userID;
            Morals = morals;
        }
        #endregion

    }
}
