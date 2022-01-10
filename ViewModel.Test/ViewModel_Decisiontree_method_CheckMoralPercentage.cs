using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Decisiontree_method_CheckMoralPercentage
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
        public void CheckOfMoralAreClose_true()
        {
            List<Moral> list1 = new List<Moral>();
            List<Moral> list2 = new List<Moral>();
            list1.Add(new Moral(1,"Slimmigheid", 100));
            list2.Add(new Moral(1,"Slimmigheid", 100));
            list1.Add(new Moral(1,"Sport", 20));
            list2.Add(new Moral(1,"Sport", 20));

            var result = Helpers.Decisiontree.Methodes.MoralPercentageCheck.CheckMoralPercentage(list1, list2);
            Assert.True(result);
        }

        [Test]
        public void CheckOfMoralAreClose_false()
        {
            List<Moral> list1 = new List<Moral>();
            List<Moral> list2 = new List<Moral>();
            list1.Add(new Moral(1, "Slimmigheid", 100));
            list2.Add(new Moral(1, "Testje", 100));
            list1.Add(new Moral(1, "Sport", 20));
            list2.Add(new Moral(1, "AnderTestje", 20));

            var result = Helpers.Decisiontree.Methodes.MoralPercentageCheck.CheckMoralPercentage(list1, list2);
            Assert.False(result);
        }

    }
}
