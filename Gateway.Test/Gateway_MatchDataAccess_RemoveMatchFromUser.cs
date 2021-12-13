using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Gateway.Test
{
    public class Gateway_MatchDataAccess_RemoveMatchFromUser
    {
        [Test]
        public void RemoveMatchFromUser_ExistingMatch_Removed()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl

            // Act
            void RemoveMatch()
            {
                try
                {
                    MatchDataAccess.AddLikeToUserIDs(userID1, userID2, 1); // Relation ship can be a random value - I chose 1
                    MatchDataAccess.RemoveMatchFromUser(userID1, userID2);
                }
                catch (Exception)
                {
                    throw;
                }
                
            }

            List<int> CheckIfNoMatches(int id)
            {
                return MatchDataAccess.GetAllMatchesFromUser(userID1, MatchOrLike.Matched);
            }

            // Assert
            Assert.DoesNotThrow(() => RemoveMatch(), "RemoveMatchFromUser");
            Assert.IsEmpty(CheckIfNoMatches(userID1), "No matches for: userID1");
            Assert.IsEmpty(CheckIfNoMatches(userID2), "No matches for: userID2");
        }
    }
}