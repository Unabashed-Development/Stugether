using Model;
using NUnit.Framework;

namespace Gateway.Test
{
    class Gateway_ProfileDataAccess_LoadRelationshipTypeMatch
    {
        /// <summary>
        /// Test whether the the right RelationshipType is returned between users 12 and 3.
        /// </summary>
        [Test]
        public void LoadRelationshipTypeOfMatch()
        {
            //setup
            Account.UserID = 12;
            //act
            string data = ProfileDataAccess.LoadRelationshipTypeMatch(3);
            //assert
            Assert.AreEqual(data, "Liefde");
        }
    }
}
