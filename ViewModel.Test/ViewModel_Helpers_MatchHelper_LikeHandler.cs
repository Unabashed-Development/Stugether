using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Gateway;
using Model;
using ViewModel.Helpers;


namespace ViewModel.Test
{
    public class ViewModel_Helpers_MatchHelper_LikeHandler
    {
        [Test]
                    
        
        public void AddLikeToMatch()
        {
            //act
            MatchHelper.LikeHandler(1, 12, 1);

            bool Liked = MatchDataAccess.CheckIfUserLiked(12, 1);
            

            //assert
            Assert.AreEqual(true, Liked, "Like is true");
        }

        [Test]
        public void MatchByLikingBack()
        {
            //act
            MatchHelper.LikeHandler(12, 1, 1);

            List<int> Match = MatchDataAccess.GetAllMatchesFromUser( 1, MatchOrLike.Matched);

            MatchDataAccess.RemoveMatchFromUser(1, 12);

            //assert
            Assert.AreEqual(12, Match[0], "The UserID matched is 12");
        }
    }
}
