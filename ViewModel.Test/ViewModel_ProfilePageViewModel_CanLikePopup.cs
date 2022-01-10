using NUnit.Framework;
using Model;

namespace ViewModel.Test
{
    class ViewModel_ProfilePageViewModel_CanLikePopup
    {
        [Test]
        public void CanLikePopup_True()
        {
            //setup
            Account.UserID = 12;
            MatchingProfilePageViewModel matchingProfilePageViewModel = new MatchingProfilePageViewModel();
            bool result;
            matchingProfilePageViewModel.OutputPopupLove = true;
            //act
            result = matchingProfilePageViewModel.CanLikePopup();
            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CanLikePopup_False()
        {
            //setup
            Account.UserID = 12;
            MatchingProfilePageViewModel matchingProfilePageViewModel = new MatchingProfilePageViewModel();
            bool result;
            //act
            result = matchingProfilePageViewModel.CanLikePopup();
            //assert
            Assert.IsFalse(result);

        }

    }
}
