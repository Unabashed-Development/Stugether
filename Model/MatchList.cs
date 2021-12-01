using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MatchList
    {
        public HashSet<Match> Matches { get; set; }
        private Student Student;

        public MatchList(Student student)
        {
            Student = student;
            UpdateMatches();
            //store all local matches here
        }

        public void UpdateMatches()
        {
            //get all matches from database
            //add them in Matches hashset
            Matches = new HashSet<Match>();
        }

        public void AddMatch(Match match)
        {
            //update database
            Matches.Add(match);
        }

        public void RemoveMatch(Match match)
        {
            Matches.Remove(match);
        }

        public Boolean IsMatch(Match match)
        {
            return Matches.Contains(match);
        }

    }
}
