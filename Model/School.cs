namespace Model
{
    public class School
    {

        public int UserID { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCity { get; set; }
        public string Study { get; set; }

        public School(int userId, string schoolName, string schoolCity, string study)
        {
            UserID = userId;
            SchoolName = schoolName;
            SchoolCity = schoolCity;
            Study = study;
        }

    }
}
