namespace Model
{
    public class Interest
    {

        public int InterestID { get; set; }
        public string InterestName { get; set; }
        public int CategoryID { get; set; }

        public Interest(int interestID, string interestName, int categoryID)
        {
            InterestID = interestID;
            InterestName = interestName;
            CategoryID = categoryID;
        }

        public override string ToString()
        {
            return InterestName;
        }

    }
}
