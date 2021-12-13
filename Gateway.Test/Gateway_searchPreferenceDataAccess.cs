using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NuGet.Frameworks;
using NUnit.Framework;

namespace Gateway.Test
{
    class Gateway_searchPreferenceDataAccess
    {
        [Test]
        public void GetPreferenceData_ObjSame_ThrowExeption()
        {
            var result = SearchPreferenceDataAccess.GetRelationType(3);
            Assert.IsInstanceOf<RelationType>(result);
        }

        [Test]
        public void GetPreferenceData_ObjNotNull_ThrowExeption()
        {
            
            var result = SearchPreferenceDataAccess.GetRelationType(3);

            Assert.NotNull(result);
        }
    }
}
