using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Method_Decisiontree_SameAge
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
        public void CheckOfAgeAreSame_true()
        {
            var pf1 = new Profile() { Age = "20 jaar"};
            var pf2 = new Profile() { Age = "21 jaar" };
            var result = Helpers.Decisiontree.Methodes.SameAgeCheck.CheckSameAge(pf1, pf2);
            Assert.True(result);
        }

        [Test]
        public void CheckOfAgeAreSame_false()
        {
            var pf1 = new Profile() { Age = "35 jaar" };
            var pf2 = new Profile() { Age = "12 jaar" };
            var result = Helpers.Decisiontree.Methodes.SameAgeCheck.CheckSameAge(pf1, pf2);
            Assert.False(result);
        }
    }
}
