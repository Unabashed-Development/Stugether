using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Test
{
    public class ViewModel_decisiontree_Method_MoralCheck
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CheckOfMoralAreClose_MoreThan5_true()
        {
            List<Moral> list1 = new List<Moral>();
            list1.Add(new Moral(1,"moral1",100));
            list1.Add(new Moral(2,"moral1",100));
            list1.Add(new Moral(3,"moral1",100));
            list1.Add(new Moral(4,"moral1",100));
            list1.Add(new Moral(5,"moral1",100));
            list1.Add(new Moral(6,"moral1",100));
            list1.Add(new Moral(7,"moral1",100));

            List<Moral> list2 = new List<Moral>();
            list2.Add(new Moral(1, "moral1", 100));
            list2.Add(new Moral(2, "moral1", 100));
            list2.Add(new Moral(3, "moral1", 100));
            list2.Add(new Moral(4, "moral1", 100));
            list2.Add(new Moral(5, "moral1", 100));
            list2.Add(new Moral(6, "moral1", 100));
            list2.Add(new Moral(8, "moral1", 100));

            var result = Helpers.Decisiontree.Methodes.MoralCheck.CheckMoral(list1, list2);
            Assert.True(result);
        }

        [Test]
        public void CheckOfMoralAreClose_More4_true()
        {

            List<Moral> list1 = new List<Moral>();
            list1.Add(new Moral(1, "moral1", 100));
            list1.Add(new Moral(2, "moral1", 100));
            list1.Add(new Moral(3, "moral1", 100));
            list1.Add(new Moral(4, "moral1", 100));

            List<Moral> list2 = new List<Moral>();
            list2.Add(new Moral(1, "moral1", 100));
            list2.Add(new Moral(2, "moral1", 100));
            list2.Add(new Moral(3, "moral1", 100));
            list2.Add(new Moral(4, "moral1", 100));
            list2.Add(new Moral(50, "moral1", 100));
            list2.Add(new Moral(60, "moral1", 100));
            list2.Add(new Moral(80, "moral1", 100));

            var result = Helpers.Decisiontree.Methodes.MoralCheck.CheckMoral(list1, list2);
            Assert.True(result);
        }
        [Test]
        public void CheckOfMoralAreClose_One_true()
        {

            List<Moral> list1 = new List<Moral>();
            list1.Add(new Moral(1, "moral1", 100));
            list1.Add(new Moral(21, "moral1", 100));
            list1.Add(new Moral(32, "moral1", 100));
            list1.Add(new Moral(43, "moral1", 100));
            list1.Add(new Moral(54, "moral1", 100));
            list1.Add(new Moral(65, "moral1", 100));
            list1.Add(new Moral(76, "moral1", 100));

            List<Moral> list2 = new List<Moral>();
            list2.Add(new Moral(1, "moral1", 100));
            list2.Add(new Moral(20, "moral1", 100));
            list2.Add(new Moral(39, "moral1", 100));
            list2.Add(new Moral(48, "moral1", 100));
            list2.Add(new Moral(57, "moral1", 100));
            list2.Add(new Moral(66, "moral1", 100));
            list2.Add(new Moral(85, "moral1", 100));

            var result = Helpers.Decisiontree.Methodes.MoralCheck.CheckMoral(list1, list2);
            Assert.True(result);
        }

        [Test]
        public void CheckOfMoralAreClose_False()
        {
            List<Moral> list1 = new List<Moral>();
            list1.Add(new Moral(1, "moral1", 100));
            list1.Add(new Moral(2, "moral1", 100));
            list1.Add(new Moral(3, "moral1", 100));


            List<Moral> list2 = new List<Moral>();
            list2.Add(new Moral(4, "moral1", 100));
            list2.Add(new Moral(5, "moral1", 100));
            list2.Add(new Moral(6, "moral1", 100));


            var result = Helpers.Decisiontree.Methodes.MoralCheck.CheckMoral(list1, list2);
            Assert.False(result);
        }
    }
}
