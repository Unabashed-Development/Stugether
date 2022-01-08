using System.Collections.Generic;
using NUnit.Framework;
using Gateway;

namespace Gateway.Test
{
    class Gateway_MatchDataAccess_GetReceivedLikesFromUser
    {
        [Test]
        public void GetReceivedLikesFromUser_One_Like()
        {
            //setup
            clean();
            MatchDataAccess.AddLikeToUserIDs(52, 51, 1);

            //act
            List<int> Liked = MatchDataAccess.GetReceivedLikesFromUser(51);

            //assert
            List<int> vs = new List<int>();
            vs.Add(52);
            Assert.AreEqual(Liked[0], vs[0]);

        }

        [Test]
        public void GetReceivedLikesFromUser_No_Likes()
        {
            //setup
            clean();
            MatchDataAccess.AddLikeToUserIDs(51, 52, 1);

            //act
            List<int> Liked = MatchDataAccess.GetReceivedLikesFromUser(51);

            //assert
            Assert.IsEmpty(Liked);
        }

        [Test]
        public void GetReceivedLikesFromUser_Two_Like()
        {
            //setup
            clean();
            MatchDataAccess.AddLikeToUserIDs(52, 51, 1);
            MatchDataAccess.AddLikeToUserIDs(53, 51, 1);

            //act
            List<int> Liked = MatchDataAccess.GetReceivedLikesFromUser(51);

            //assert
            List<int> vs = new List<int>();
            vs.Add(52);
            vs.Add(53);
            Assert.AreEqual(Liked[0], vs[0]);

        }

        [Test]
        public void GetReceivedLikesFromuser_One_Like_One_Match()
        {
            //setup
            clean();
            MatchDataAccess.AddLikeToUserIDs(52, 51, 1);
            MatchDataAccess.AddLikeToUserIDs(53, 51, 1);
            MatchDataAccess.SetMatchToUserIDs(53, 51);

            //act
            List<int> Liked = MatchDataAccess.GetReceivedLikesFromUser(51);

            //assert
            List<int> vs = new List<int>();
            vs.Add(52);
            Assert.AreEqual(Liked[0], vs[0]);
        }

        public static void clean()
        {
            MatchDataAccess.RemoveMatchFromUser(51, 52);
            MatchDataAccess.RemoveMatchFromUser(51, 53);
        }
    }
}
