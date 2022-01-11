using System.Collections.Generic;
using System.Linq;

namespace Model
{
    /// <summary>
    /// Holds the data for all the morals of a profile
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
            morals.OrderBy(moral => moral.MoralID);
        }
        #endregion

    }
}
