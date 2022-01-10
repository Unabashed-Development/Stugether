using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NuGet.Frameworks;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_MainDecisiontree_GetListReturned
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
        public void GetListOfPotentialMatches_NotNull()
        {
            var result = Helpers.Decisiontree.MainDecisionTree.GetProfilesBasedOnIntrestAndNormsAndValues(Account.UserID.Value);
            Assert.NotNull(result);
        }
    }
}
