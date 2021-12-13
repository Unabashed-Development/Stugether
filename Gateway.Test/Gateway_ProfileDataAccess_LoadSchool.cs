using Model;
using NUnit.Framework;

namespace Gateway.Test
{
    class Gateway_ProfileDataAccess_LoadSchool
    {

        [Test]
        public void CheckIfSchoolIsNull()
        {
            //data
            School data = ProfileDataAccess.LoadSchool(-1);

            //expected value is null (userID -1 doesnt exists)
            Assert.IsNull(data);
        }

        [Test]
        public void CheckIfInterestsIsNotEmpty()
        {
            //data
            School data = ProfileDataAccess.LoadSchool(3);

            //expected value is not null, User with id 3 has always a school
            Assert.IsNotNull(data);
        }

    }
}
