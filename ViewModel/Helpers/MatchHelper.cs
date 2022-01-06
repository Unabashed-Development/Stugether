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
            List<int> matchedIDs = MatchDataAccess.GetAllMatchesFromUser(userID, MatchOrLike.Matched);
            return (from int id in matchedIDs
                    select ProfileDataAccess.LoadProfile(id)).ToList();
        }

        public static List<Profile> LoadProfilesOfLikes(int userID)
        {
            List<int> likedIDs = MatchDataAccess.GetReceivedLikesFromUser(userID);
            return (from int id in likedIDs
                    select ProfileDataAccess.LoadProfile(id)).ToList();
        }

        /// <summary>
        /// Handles whether the likedUser is liked in the data base or gets matched, based on whether the user has already been liked by the likedUser
        /// </summary>
        /// <param name="userID">UserID from the person whos liking.</param>
        /// <param name="likedUserID">UserID from the person whos getting liked.</param>
        /// <param name="relationshipTypeID">The relationshipTypeID from the relationshipType the likeduser is getting liked on.</param>
        public static void LikeHandler(int userID, int likedUserID, int relationshipTypeID)
        {
            if (MatchDataAccess.CheckIfUserLiked(userID, likedUserID))
            {
                MatchDataAccess.SetMatchToUserIDs(userID, likedUserID);
            }
            else
            {
                MatchDataAccess.AddLikeToUserIDs(userID, likedUserID, relationshipTypeID);
            }
        }
    }
}