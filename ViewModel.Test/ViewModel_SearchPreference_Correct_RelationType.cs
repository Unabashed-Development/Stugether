using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Test
{
    public class ViewModel_SearchPreference_Correct_RelationType
    {
        [SetUp]
        public void Setup()
        {
            InitialSetupForTests.ClearFieldsInAccount();
            Account.Email = "test@mail.com";
            Account.Authenticated = true;
            Account.UserID = 1;
        }


        [Test]
        public void CheckIfRelationTypeMatches()
        {
            SearchPreferencePageViewModel vm = new SearchPreferencePageViewModel();
            Assert.AreEqual(true, vm.Love);
            Assert.AreEqual(true, vm.Business);
            Assert.AreEqual(false, vm.Friend);
            Assert.AreEqual(false, vm.StudyBuddy);
        }
    }
}
