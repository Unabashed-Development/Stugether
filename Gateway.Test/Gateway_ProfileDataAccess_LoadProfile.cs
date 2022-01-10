using Model;
using NUnit.Framework;

namespace Gateway.Test
{
    class Gateway_ProfileDataAccess_LoadProfile
    {

        [Test]
        public void CheckIfProfileIsNull()
        {
            //data
            Profile profile = ProfileDataAccess.LoadProfile(-1);

            //expected value is null (userID -1 doesnt exists)
            Assert.IsNull(profile);
        }

        [Test]
        public void CheckIfProfileIsNotNull()
        {
            //prepare
            Account.UserID = 3;

            //data
            Profile profile = ProfileDataAccess.LoadProfile(Account.UserID.Value);

            //expected value is not null, User with id 3 does exists
            Assert.IsNotNull(profile);
        }

    }
}
