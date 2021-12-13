using System;

namespace Model
{
    /// <summary>
    /// Holds the data of a single interest, with the Id, name and in which category id it belongs
    /// </summary>
    public class Interest
    {

        #region fields
        public int InterestID { get; set; }
        public string InterestName { get; set; }
        public int CategoryID { get; set; }
        #endregion

        #region constructors
        public Interest(int interestID, string interestName, int categoryID)
        {
            InterestID = interestID;
            InterestName = interestName;
            CategoryID = categoryID;
        }
        #endregion

        #region methods
        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Interest) && ((Interest)obj).InterestID == InterestID;
        }

        public override string ToString()
        {
            return InterestName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(InterestID);
        }
        #endregion
    }
}
