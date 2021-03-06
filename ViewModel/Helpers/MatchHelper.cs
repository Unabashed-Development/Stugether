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
            List<Profile> matchesList = new List<Profile>();
            List<int> matchedIDs = MatchDataAccess.GetAllMatchesFromUser(userID, MatchOrLike.Matched);
            matchesList = (from int id in matchedIDs
                           select ProfileDataAccess.LoadProfile(id)).ToList();
            matchesList = matchesList.Select((p) => { p.UpdateUnreadMessages(userID); return p; }).ToList(); ;
            return NotificationHelper.FixBirthdayPreferences(matchesList);
        }

        public static List<Profile> LoadProfilesOfLikes(int userID)
        {
            List<Profile> likesList = new List<Profile>();
            List<int> likedIDs = MatchDataAccess.GetReceivedLikesFromUser(userID);
            likesList = (from int id in likedIDs
                         select ProfileDataAccess.LoadProfile(id)).ToList();
            return NotificationHelper.FixBirthdayPreferences(likesList);
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

        public static bool CheckForExistingLike(int userID, int likedUserID)
        {
            bool liked = MatchDataAccess.CheckIfUserLiked(userID, likedUserID);
            return liked;
        }

        public static List<int> RelationshipHandler(int userID, int likedUserID)
        {
            List<int> EqualRT = new List<int>();
            if (!CheckForExistingLike(userID, likedUserID))
            {
                RelationType RT1 = SearchPreferenceDataAccess.GetRelationType(userID);
                RelationType RT2 = SearchPreferenceDataAccess.GetRelationType(likedUserID);                
                if (RT1.Love == true && RT2.Love == true)
                {
                    EqualRT.Add(1);
                }
                if (RT1.Business == true && RT2.Business == true)
                {
                    EqualRT.Add(2);
                }
                if (RT1.StudyBuddy == true && RT2.StudyBuddy == true)
                {
                    EqualRT.Add(3);
                }
                if (RT1.Friend == true && RT2.Friend == true)
                {
                    EqualRT.Add(4);
                }
            }
            else
            {
                EqualRT.Add(0);
            }
            return EqualRT;
        } 
    }
}