using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_Decisiontree_Method_SameSchool
    {
        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public void CheckOfSchoolAreSame_true()
        {
            var school1 = new School(1, "Windesheim", "Zwolle", "Verf");
            var school2 = new School(1, "Windesheim", "Zwolle", "Verf");
            var result = Helpers.Decisiontree.Methodes.SameSchoolCheck.CheckSameSchool(school1, school2);
            Assert.True(result);
        }

        [Test]
        public void CheckOfSchoolAreSame_false()
        {
            var school1 = new School(1, "Windesheim", "Zwolle","Verf");
            var school2 = new School(1, "Johannus college", "Zwolle","Verf");
            var result = Helpers.Decisiontree.Methodes.SameSchoolCheck.CheckSameSchool(school1, school2);
            Assert.False(result);
        }

        [Test]
        public void CheckOfSchoolCanBeNull()
        {
            var school1 = new School(1, "Windesheim", "Zwolle", "Verf");
            var result = Helpers.Decisiontree.Methodes.SameSchoolCheck.CheckSameSchool(null, school1);
            Assert.False(result);
        }
    }
}
