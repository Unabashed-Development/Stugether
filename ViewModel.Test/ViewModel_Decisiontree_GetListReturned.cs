using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NuGet.Frameworks;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Decisiontree_GetListReturned
    {
        [SetUp]
        public void Setup()
        {
            InitialSetupForTests.ClearFieldsInAccount();
            Account.Email = "UnitTest1@windesheim.nl";
            Account.Authenticated = true;
            Account.UserID = 176;
        }


        [Test]
        public void GetListOfPotentialMatches_NotNull()
        {
            var result = Helpers.Decisiontree.MainDecisionTree.GetProfilesBasedOnIntrestAndNormsAndValues(Account.UserID.Value);
            Assert.NotNull(result);
        }


        [Test]
        public void AccountUnitTestInTopList()
        {
            Helpers.Decisiontree.MainDecisionTree.GetProfilesBasedOnIntrestAndNormsAndValues(Account.UserID.Value);
            var result = Helpers.Decisiontree.MainDecisionTree.BestMatch;
            Assert.That(result.Any(p => p.LastName.Equals("Test2")));
            Assert.That(result.Any(p => p.LastName.Equals("Test3")));
        }
    }
}
