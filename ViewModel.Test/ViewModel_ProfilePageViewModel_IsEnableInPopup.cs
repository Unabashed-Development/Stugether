using System.Collections.Generic;
using NUnit.Framework;
using Model;

namespace ViewModel.Test
{
    class ViewModel_ProfilePageViewModel_IsEnableInPopup
    {
        [Test]
        public void IsEnableInPopup_OneInList()
        {
            //setup
            Account.UserID = 12;
            MatchingProfilePageViewModel matchingProfilePageViewModel = new MatchingProfilePageViewModel();
            List<int> intList = new List<int>();
            intList.Add(1);
            //act
            matchingProfilePageViewModel.IsEnabledInPopup(intList);
            //assert
            Assert.IsTrue(matchingProfilePageViewModel.IsEnabledLove);
            Assert.IsFalse(matchingProfilePageViewModel.IsEnabledBusiness);
            Assert.IsFalse(matchingProfilePageViewModel.IsEnabledStudyBuddy);
            Assert.IsFalse(matchingProfilePageViewModel.IsEnabledFriend);
        }

        [Test]
        public void IsEnableInPopup_AllInList()
        {
            //setup
            Account.UserID = 12;
            MatchingProfilePageViewModel matchingProfilePageViewModel = new MatchingProfilePageViewModel();
            List<int> intList = new List<int>();
            intList.Add(1);
            intList.Add(2);
            intList.Add(3);
            intList.Add(4);
            //act
            matchingProfilePageViewModel.IsEnabledInPopup(intList);
            //assert
            Assert.IsTrue(matchingProfilePageViewModel.IsEnabledLove);
            Assert.IsTrue(matchingProfilePageViewModel.IsEnabledBusiness);
            Assert.IsTrue(matchingProfilePageViewModel.IsEnabledStudyBuddy);
            Assert.IsTrue(matchingProfilePageViewModel.IsEnabledFriend);
        }
    }
}
