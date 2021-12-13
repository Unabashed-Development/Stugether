using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Gateway.Test
{
    public class Gateway_MatchDataAccess_AddLikeToUserIDs
    {
        static void CleanUp(int id1, int id2)
        {
            MatchDataAccess.RemoveMatchFromUser(id1, id2);
        }

        [Test]
        public void AddLikeToUserIDs_NonExistingMatch_ThrowsNoError()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl

            // Act
            void AddLike()
            {
                try
                {
                    MatchDataAccess.AddLikeToUserIDs(userID1, userID2, 1); // Relation ship can be a random value - I chose 1
                }
                catch (Exception)
                {
                    throw;
                }
                
            }

            // Assert
            Assert.DoesNotThrow(() => AddLike(), "AddLikeToUserIDs");
            Assert.DoesNotThrow(() => CleanUp(userID1, userID2), "RemoveMatchFromUser");
        }

        [Test]
        public void AddLikeToUserIDs_ExistingMatch_ThrowsError()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl

            // Act
            void AddLike()
            {
                try
                {
                    MatchDataAccess.AddLikeToUserIDs(userID1, userID2, 1); // Relation ship can be a random value - I chose 1
                }
                catch (Exception)
                {
                    throw;
                }

            }

            // Assert
            Assert.DoesNotThrow(() => AddLike(), "Should not throw exception");
            Assert.Throws<System.Data.SqlClient.SqlException>(() => AddLike(), "Should throw exception");
            Assert.DoesNotThrow(() => CleanUp(userID1, userID2), "RemoveMatchFromUser");
        }
    }
}