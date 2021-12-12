using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Gateway.Test
{
    public class Gateway_MatchDataAccess_GetAllMatchesFromUser_and_RemoveMatchFromUser
    {
        [Test]
        public void GetAllMatchesFromUser_and_RemoveMatchFromUser_NewMatchAndRemoveIt_ThrowsNoErrors()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl

            // Act
            List<int> listOfMatches = MatchDataAccess.GetAllMatchesFromUser(userID1);

            void AddLike()
            {
                MatchDataAccess.AddLikeToUserIDs(userID1, userID2, 1); // Relation ship can be a random value - I chose 1
            }

            List<int> GetListOfMatches()
            {
                try
                {
                    return MatchDataAccess.GetAllMatchesFromUser(userID1);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            void RemoveMatchFromUser()
            {
                try
                {
                    MatchDataAccess.RemoveMatchFromUser(userID1, userID2);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // Assert
            Assert.DoesNotThrow(() => AddLike(), "AddLikeToUserIDs");
            Assert.DoesNotThrow(() => GetListOfMatches(), "GetAllMatchesFromUser");
            Assert.DoesNotThrow(() => RemoveMatchFromUser(), "RemoveMatchFromUser()");
        }
    }
}