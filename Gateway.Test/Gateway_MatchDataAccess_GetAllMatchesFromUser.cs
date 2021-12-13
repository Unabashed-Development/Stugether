using System;
using NUnit.Framework;
using System.Collections.Generic;
using Model;
using System.Linq;

namespace Gateway.Test
{
    public class Gateway_MatchDataAccess_GetAllMatchesFromUser
    {
        [Test]
        public void GetAllMatchesFromUser_OneMatch_ReturnsListOfMatch()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl
            int? matchedUserID = null;

            // Act
            List<int> listOfMatches = MatchDataAccess.GetAllMatchesFromUser(userID1);

            List<int> GetListOfMatches()
            {
                try
                {
                    MatchDataAccess.AddLikeToUserIDs(userID1, userID2, 1); // First add the match to the database
                    MatchDataAccess.SetMatchToUserIDs(userID2, userID1); // Set them to the matching state and turn them around just to test
                    List<int> result = MatchDataAccess.GetAllMatchesFromUser(userID1);
                    matchedUserID = result.First();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            void CleanUp()
            {
                MatchDataAccess.RemoveMatchFromUser(userID1, userID2); // Clean up the match
            }

            // Assert
            Assert.DoesNotThrow(() => GetListOfMatches(), "GetAllMatchesFromUser");
            Assert.AreEqual(12, matchedUserID, "Is the matched result correct?");
            Assert.DoesNotThrow(() => CleanUp(), "Clean up");
        }
    }
}