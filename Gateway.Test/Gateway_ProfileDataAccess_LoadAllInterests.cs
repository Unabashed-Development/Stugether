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
            List<Interest> list = ProfileDataAccess.LoadAllInterests();
            Assert.IsNotEmpty(list);
        }

    }
}
