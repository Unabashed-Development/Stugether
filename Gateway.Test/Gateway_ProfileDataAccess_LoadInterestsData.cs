using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Test
{
    class Gateway_ProfileDataAccess_LoadInterestsData
    {

        [Test]
        public void CheckIfInterestsIsEmpty()
        {
            //data
            InterestsData data = ProfileDataAccess.LoadInterestsData(-1);

            //expected value is empty (userID -1 doesnt exists)
            Assert.IsEmpty(data.Interests);
        }

        [Test]
        public void CheckIfInterestsIsNotEmpty()
        {
            //data
            InterestsData data = ProfileDataAccess.LoadInterestsData(3);

            //expected value is not empty, User with id 3 has always interests
            Assert.IsNotEmpty(data.Interests);
        }

    }
}
