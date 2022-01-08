using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Gateway.Test
{
    public class Gateway_MatchDataAccess_SetMatchToUserID
    {
        [Test]
        public void SetMatchToUserID_OneMatch_ThrowsNoError()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl

            // Act
            List<int> listOfMatches = MatchDataAccess.GetAllMatchesFromUser(userID1, MatchOrLike.Matched);

            void MakeMatch()
            {
                try
                {
                    MatchDataAccess.AddLikeToUserIDs(userID1, userID2, 1); // First add the match to the database
                    MatchDataAccess.SetMatchToUserIDs(userID1, userID2); // Set them to the matching state
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
            Assert.DoesNotThrow(() => MakeMatch(), "SetMatchToUserIDs");
            Assert.DoesNotThrow(() => CleanUp(), "Clean up");
        }
    }
}