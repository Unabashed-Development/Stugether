using Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace Gateway.Test
{
    class Gateway_ProfileDataAccess_LoadAllInterests
    {

        [Test]
        public void CheckIfInterestsExists()
        {
            //data
            List<Interest> list = ProfileDataAccess.LoadAllInterests();

            //expected value is not empty list
            Assert.IsNotEmpty(list);
        }

    }
}
