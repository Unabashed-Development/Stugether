using NUnit.Framework;
using Gateway;
using ViewModel;
using Model;
using System.Collections.Generic;

namespace ViewModel.Test
{
    class ViewModel_Helpers_MatchHelper_RelationshipHandler
    {
        [Test]
        public void RelationshipHandler_OneTypeEquals()
        {
            //setup
            List<int> result = new List<int>();
            RelationType User1 = new RelationType();
            RelationType User2 = new RelationType();
            User1.Friend = true;
            User2.Friend = true;
            User1.Business = true;
            User2.StudyBuddy = true;
            SearchPreferenceDataAccess.SaveRelationPreference(User1, 51);
            SearchPreferenceDataAccess.SaveRelationPreference(User2, 54);
            //act
            result = Helpers.MatchHelper.RelationshipHandler(51, 54);
            //assert            
            Assert.AreEqual(result[0], 4);
        }

        [Test]
        public void RelationshipHandler_TwoTypesEquals()
        {
            //setup
            List<int> result = new List<int>();
            RelationType User1 = new RelationType();
            RelationType User2 = new RelationType();
            User1.Friend = true;
            User2.Friend = true;
            User1.Business = true;
            User2.Business = true;
            User2.StudyBuddy = true;
            SearchPreferenceDataAccess.SaveRelationPreference(User1, 51);
            SearchPreferenceDataAccess.SaveRelationPreference(User2, 54);
            //act
            result = Helpers.MatchHelper.RelationshipHandler(51, 54);
            //assert            
            Assert.AreEqual(result[0], 2);
            Assert.AreEqual(result[1], 4);
        }

        [Test]
        public void RelationshipHandler_AllTypesEquals()
        {
            //setup
            List<int> result = new List<int>();
            RelationType User1 = new RelationType();
            RelationType User2 = new RelationType();

            User1.Love = true;
            User2.Love = true;
            User1.Business = true;
            User2.Business = true;
            User1.StudyBuddy = true;
            User2.StudyBuddy = true;
            User1.Friend = true;
            User2.Friend = true;
            
            SearchPreferenceDataAccess.SaveRelationPreference(User1, 51);
            SearchPreferenceDataAccess.SaveRelationPreference(User2, 54);
            //act
            result = Helpers.MatchHelper.RelationshipHandler(51, 54);
            //assert            
            Assert.AreEqual(result[0], 1);
            Assert.AreEqual(result[1], 2);
            Assert.AreEqual(result[2], 3);
            Assert.AreEqual(result[3], 4);

        }
        [Test]
        public void RelationshipHandler_TwoTypesEquals_AlreadyLiked()
        {
            //setup
            List<int> result = new List<int>();
            MatchDataAccess.AddLikeToUserIDs(54, 51, 1);
            RelationType User1 = new RelationType();
            RelationType User2 = new RelationType();
            User1.Friend = true;
            User2.Friend = true;
            User1.Business = true;
            User2.Business = true;
            User2.StudyBuddy = true;
            SearchPreferenceDataAccess.SaveRelationPreference(User1, 51);
            SearchPreferenceDataAccess.SaveRelationPreference(User2, 54);
            //act
            result = Helpers.MatchHelper.RelationshipHandler(51, 54);
            //assert
            Assert.AreEqual(result[0], 0);
            //cleanup
            MatchDataAccess.RemoveMatchFromUser(54, 51);

        }       
    }
}