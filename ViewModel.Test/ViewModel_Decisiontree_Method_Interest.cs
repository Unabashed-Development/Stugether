using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Test
{
    class ViewModel_Decisiontree_Method_Interest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CheckOfInterstAreClose_MoreThan5_true()
        {
            List<Interest> list1 = new List<Interest>();
            list1.Add(new Interest(1,"werken",1));
            list1.Add(new Interest(2,"lopen",1));
            list1.Add(new Interest(3,"slaan",1));
            list1.Add(new Interest(4,"eten",1));
            list1.Add(new Interest(5,"koken",1));
            list1.Add(new Interest(6,"meerwerken",1));

            List<Interest> list2 = new List<Interest>();
            list2.Add(new Interest(1, "werken", 1));
            list2.Add(new Interest(2, "lopen", 1));
            list2.Add(new Interest(3, "slaan", 1));
            list2.Add(new Interest(4, "eten", 1));
            list2.Add(new Interest(5, "koken", 1));
            list2.Add(new Interest(6, "meerwerken", 1));

            var result = Helpers.Decisiontree.Methodes.InterestCheck.CheckInterest(list1, list2);
            Assert.True(result);
        }

        [Test]
        public void CheckOfInterestAreClose_More4_true()
        {

            List<Interest> list1 = new List<Interest>();
            list1.Add(new Interest(1, "werken", 1));
            list1.Add(new Interest(2, "lopen", 1));
            list1.Add(new Interest(3, "slaan", 1));
            list1.Add(new Interest(4, "eten", 1));
            list1.Add(new Interest(50, "koken", 1));
            list1.Add(new Interest(60, "meerwerken", 1));

            List<Interest> list2 = new List<Interest>();
            list2.Add(new Interest(1, "werken", 1));
            list2.Add(new Interest(2, "lopen", 1));
            list2.Add(new Interest(3, "slaan", 1));
            list2.Add(new Interest(4, "eten", 1));
            list2.Add(new Interest(51, "koken", 1));
            list2.Add(new Interest(61, "meerwerken", 1));

            var result = Helpers.Decisiontree.Methodes.InterestCheck.CheckInterest(list1, list2);
            Assert.True(result);
        }
        [Test]
        public void CheckOfInterestAreClose_One_true()
        {

            List<Interest> list1 = new List<Interest>();
            list1.Add(new Interest(1, "werken", 1));
            list1.Add(new Interest(21, "lopen", 1));
            list1.Add(new Interest(32, "slaan", 1));
            list1.Add(new Interest(43, "eten", 1));
            list1.Add(new Interest(54, "koken", 1));
            list1.Add(new Interest(65, "meerwerken", 1));

            List<Interest> list2 = new List<Interest>();
            list2.Add(new Interest(1, "werken", 1));
            list2.Add(new Interest(25, "lopen", 1));
            list2.Add(new Interest(34, "slaan", 1));
            list2.Add(new Interest(43, "eten", 1));
            list2.Add(new Interest(52, "koken", 1));
            list2.Add(new Interest(61, "meerwerken", 1));

            var result = Helpers.Decisiontree.Methodes.InterestCheck.CheckInterest(list1, list2);
            Assert.True(result);
        }

        [Test]
        public void CheckOfInterestAreClose_False()
        {
            List<Interest> list1 = new List<Interest>();
            list1.Add(new Interest(1, "werken", 1));
            list1.Add(new Interest(2, "lopen", 1));
            list1.Add(new Interest(3, "slaan", 1));
            list1.Add(new Interest(4, "eten", 1));
            list1.Add(new Interest(5, "koken", 1));
            list1.Add(new Interest(6, "meerwerken", 1));

            List<Interest> list2 = new List<Interest>();
            list2.Add(new Interest(11, "werken", 1));
            list2.Add(new Interest(21, "lopen", 1));
            list2.Add(new Interest(31, "slaan", 1));
            list2.Add(new Interest(41, "eten", 1));
            list2.Add(new Interest(51, "koken", 1));
            list2.Add(new Interest(61, "meerwerken", 1));

            var result = Helpers.Decisiontree.Methodes.InterestCheck.CheckInterest(list1, list2);
            Assert.False(result);
        }
    }
}
