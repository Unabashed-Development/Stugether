using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Method_Decisiontree_SameCity
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
        public void CheckOfCityAreSame_true()
        {
            var pf1 = new Profile() { City = "Amsterdam" };
            var pf2 = new Profile() { City = "Amsterdam" };
            var result = Helpers.Decisiontree.Methodes.SameCityCheck.CheckSameCity(pf1,pf2);
            Assert.True(result);
        }

        [Test]
        public void CheckOfCityAreSame_false()
        {
            var pf1 = new Profile() { City = "Amsterdam" };
            var pf2 = new Profile() { City = "Rotterdam" };
            var result = Helpers.Decisiontree.Methodes.SameCityCheck.CheckSameCity(pf1, pf2);
            Assert.False(result);
        }
    }
}
