using System.Collections.Generic;

namespace Model
{
    public class InterestsData
    {

        public int UserID { get; set; }
        public List<Interest> Interests { get; set; }

        public InterestsData(int userID, List<Interest> interest)
        {
            UserID = userID;
            Interests = interest;
        }

    }
}
