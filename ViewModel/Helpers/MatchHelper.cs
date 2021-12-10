using Gateway;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Helpers
{
    public static class MatchHelper
    {
        public static List<Profile> LoadProfilesOfMatches(int userID)
        {
            List<int> matchedIDs = MatchDataAccess.GetAllMatchesFromUser(userID);
            List<Profile> matchedProfiles = new List<Profile>();
            foreach (int id in matchedIDs)
            {
                matchedProfiles.Add(ProfileDataAccess.LoadProfile(id));
            }
            return matchedProfiles;
        }
    }
}
