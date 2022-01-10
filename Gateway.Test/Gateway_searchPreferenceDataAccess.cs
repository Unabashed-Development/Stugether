using Model;
using NUnit.Framework;

namespace Gateway.Test
{
    class Gateway_SearchPreferenceDataAccess
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
