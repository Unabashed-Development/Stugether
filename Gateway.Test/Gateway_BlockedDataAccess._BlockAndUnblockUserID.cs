using System.Collections.Generic;
using NUnit.Framework;

namespace Gateway.Test
{
    class Gateway_BlockedDataAccess
    {
        [Test]
        public void BlockUserID_one_block_Disliked()
        {
            //act
            BlockedDataAccess.BlockUserID(51, 52, BlockReason.Disliked);
            List<int> disliked = BlockedDataAccess.GetAllBlockedIDsFromUser(51, BlockReason.Disliked);
            List<int> unmatched = BlockedDataAccess.GetAllBlockedIDsFromUser(51, BlockReason.Unmatched);
            List<int> blocked = BlockedDataAccess.GetAllBlockedIDsFromUser(51, BlockReason.Blocked);

            //assert
            Assert.AreEqual(52, disliked[0]);
            Assert.IsEmpty(unmatched);
            Assert.IsEmpty(blocked);
        }

        [Test]
        public void UnblockUserID_one_unblock_Disliked()
        {
            //setup
            BlockedDataAccess.BlockUserID(51, 52, BlockReason.Disliked);
            List<int> disliked = BlockedDataAccess.GetAllBlockedIDsFromUser(51, BlockReason.Disliked);
            Assert.AreEqual(52, disliked[0]);

            //act
            BlockedDataAccess.UnblockUserID(51, 52);
            disliked = BlockedDataAccess.GetAllBlockedIDsFromUser(51, BlockReason.Disliked);

            //assert
            Assert.IsEmpty(disliked);
        }

        [Test]
        public void UnblockAllUserIDsForReason_two_unblock_Disliked()
        {
            //setup
            BlockedDataAccess.UnblockAllUserIDsForReason(51, BlockReason.Disliked);
            BlockedDataAccess.UnblockAllUserIDsForReason(51, BlockReason.Unmatched);
            BlockedDataAccess.BlockUserID(51, 52, BlockReason.Disliked);
            BlockedDataAccess.BlockUserID(51, 53, BlockReason.Disliked);
            BlockedDataAccess.BlockUserID(51, 52, BlockReason.Unmatched);

            //act
            BlockedDataAccess.UnblockAllUserIDsForReason(51, BlockReason.Disliked);
            List<int> disliked = BlockedDataAccess.GetAllBlockedIDsFromUser(51, BlockReason.Disliked);
            List<int> unmatched = BlockedDataAccess.GetAllBlockedIDsFromUser(51, BlockReason.Unmatched);

            //assert
            Assert.IsEmpty(disliked);
            Assert.AreEqual(52, unmatched[0]);
        }


    }
}
