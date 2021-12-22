namespace Model
{
    public class Moral
    {

        #region properties
        public int MoralID { get; set; }
        public string MoralName { get; set; }
        public int Percentage { get; set; }
        #endregion

        #region constructors
        public Moral(int moralID, string moralName, int percentage)
        {
            MoralID = moralID;
            MoralName = moralName;
            Percentage = percentage;
        }
        #endregion

        #region methods
        public override string ToString()
        {
            return MoralName;
        }
        #endregion

    }
}
