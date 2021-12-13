using NUnit.Framework;
using ViewModel.Helpers;
using Gateway;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel.Test
{
    public class ViewModel_Helpers_MatchHelper_LoadProfilesOfMatches
    {
        [Test]
        public void LoadProfilesOfMatches_OneMatch_ReturnsCorrectProfileInList()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl
            MatchDataAccess.AddLikeToUserIDs(userID1, userID2, 1); // RelationshipTypeID can be anything, so I put it at 1
            MatchDataAccess.SetMatchToUserIDs(userID2, userID1); // Match them, but turned variables around, to test that

            // Act
            List<Profile> validEmail = MatchHelper.LoadProfilesOfMatches(userID1);

            void CleanUp()
            {
                MatchDataAccess.RemoveMatchFromUser(userID1, userID2); // Clean up the match
            }

            // Assert
            Assert.IsNotEmpty(validEmail, "List is not empty");
            Assert.AreEqual(1, validEmail.Count, "List has one value");
            Assert.AreEqual(12, validEmail.First().UserID, "Profile has the correct user ID");
            Assert.DoesNotThrow(() => CleanUp(), "Clean up");
        }
    }
}