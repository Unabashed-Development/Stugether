using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Gateway.Test
{
    class Gateway_SearchProfileDataAccess
    {
        [Test]
        public void GetListBasedOnRelation_GetNotNull_ThrowExeption()
        {
            Assert.NotNull(SearchProfileDataAccess.GetProfileBasedOnRelationType(3));
        }

        [Test]
        public void GetListBasedOnIntrest_GetNotNull_ThrowExeption()
        {
            Assert.NotNull(SearchProfileDataAccess.GetProfileBasedOnIntrest(3));
        }
    }
}
