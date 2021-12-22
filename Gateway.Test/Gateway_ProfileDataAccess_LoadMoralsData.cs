using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Test
{
    class Gateway_ProfileDataAccess_LoadMoralsData
    {

        [Test]
        public void CheckIfMoralsDataIsEmpty()
        {
            //data
            MoralsData data = ProfileDataAccess.LoadMoralsData(-1);

            //expected value is 0 (userID -1 doesnt exists)
            Assert.IsEmpty(data.Morals);
        }

        [Test]
        public void CheckIfInterestsIsNotEmpty()
        {
            //data
            MoralsData data = ProfileDataAccess.LoadMoralsData(3);

            //expected value is not null, User with id 3 has always a school
            Assert.IsNotEmpty(data.Morals);
        }

    }
}
