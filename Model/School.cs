namespace Model
{
    public class School
    {

        public string SchoolName { get; set; }
        public string SchoolCity { get; set; }
        public string Study { get; set; }

        public School(string name, string city, string study)
        {
            SchoolName = name;
            SchoolCity = city;
            Study = study;
        }

    }
}
