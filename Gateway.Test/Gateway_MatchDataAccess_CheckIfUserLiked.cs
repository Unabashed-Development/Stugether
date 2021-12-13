using System;
using NUnit.Framework;
using System.Collections.Generic;
using Model;
using System.Linq;

namespace Gateway.Test
{
    public class Gateway_MatchDataAccess_CheckIfUserLiked
    {
        [Test]
        public void CheckIfUserLiked_DidLike_ReturnsTrue()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl
            bool? result = null;

            // Act
            List<int> listOfMatches = MatchDataAccess.GetAllMatchesFromUser(userID1, MatchOrLike.Matched);

            void CheckLike()
            {
                try
                {
                    MatchDataAccess.AddLikeToUserIDs(userID1, userID2, 1); // First add the match to the database
                    result = MatchDataAccess.CheckIfUserLiked(userID2, userID1); // Turn them around just to test
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
            Assert.DoesNotThrow(() => CheckLike(), "CheckIfUserLiked");
            Assert.IsTrue(result, "Liked?");
            Assert.DoesNotThrow(() => CleanUp(), "Clean up");
        }

        [Test]
        public void CheckIfUserLiked_DidNotLike_ReturnsFalse()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl
            bool? result = null;

            // Act
            List<int> listOfMatches = MatchDataAccess.GetAllMatchesFromUser(userID1, MatchOrLike.Matched);

            void CheckLike()
            {
                try
                {
                    result = MatchDataAccess.CheckIfUserLiked(userID1, userID2);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // Assert
            Assert.DoesNotThrow(() => CheckLike(), "CheckIfUserLiked");
            Assert.IsFalse(result, "Liked?");
        }
    }
}