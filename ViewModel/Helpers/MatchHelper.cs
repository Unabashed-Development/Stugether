using Gateway;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel.Helpers
{
    public static class MatchHelper
    {
        /// <summary>
        /// Loads all the profiles of the matches of an user ID.
        /// </summary>
        /// <param name="userID">The user ID to load the matches for.</param>
        /// <returns>A list of matched profiles.</returns>
        public static List<Profile> LoadProfilesOfMatches(int userID)
        {
            List<int> matchedIDs = MatchDataAccess.GetAllMatchesFromUser(userID);
            return (from int id in matchedIDs
                    select ProfileDataAccess.LoadProfile(id)).ToList();
        }
    }
}