using Gateway;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_OverviewMatchesViewModel_UnmatchCommand
    {
        [Test]
        public void UnmatchCommand_UnmatchOneExistingMatch_NoMatchesLeft()
        {
            // Arrange
            const int userID1 = 1; // ThisAccountDoesExist@wafoe.nl
            const int userID2 = 12; // ThisUnverifiedAccountDoesExist@wafoe.nl
            Account.UserID = userID1;
            MatchDataAccess.AddLikeToUserIDs(userID1, userID2, 1); // Set the RelationshipTypeID to a random value, 1 in this case
            OverviewMatchesViewModel viewModel = new OverviewMatchesViewModel();

            // Act
            void Unmatch()
            {
                viewModel.UnmatchCommand.Execute(userID2); // We want to unmatch userID2
            }

            bool CheckIfNoMatches() => MatchDataAccess.GetAllMatchesFromUser(userID1, MatchOrLike.Matched).Count != 1;

            // Assert
            Assert.DoesNotThrow(() => Unmatch(), "Unmatch userID2 from userID1");
            Assert.IsTrue(CheckIfNoMatches(), "There are no matches left for userID1");
        }
    }
}