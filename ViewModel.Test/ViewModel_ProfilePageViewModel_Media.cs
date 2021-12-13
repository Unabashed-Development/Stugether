using Gateway;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Test
{
    public class ViewModel_ProfilePageViewModel_Media
    {

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ProfilePageViewModel_PhotoNavigation(int listid)
        {
            List<Uri> usermedia;
            Uri shouldNextResult, shouldPrevResult;
            switch (listid)
            {
                case 0:
                default:
                    usermedia = new List<Uri>()
                    {
                    };
                    shouldNextResult = null;
                    shouldPrevResult = null;
                    break;
                case 1:
                    usermedia = new List<Uri>()
                    {
                        new Uri("aa", UriKind.RelativeOrAbsolute)
                    };
                    shouldPrevResult = new Uri("aa", UriKind.RelativeOrAbsolute);
                    shouldNextResult = new Uri("aa", UriKind.RelativeOrAbsolute);
                    break;
                case 2:
                    usermedia = new List<Uri>() 
                    {
                        new Uri("aa", UriKind.RelativeOrAbsolute),
                        new Uri("bb", UriKind.RelativeOrAbsolute)
                    };
                    shouldNextResult = new Uri("bb", UriKind.RelativeOrAbsolute);
                    shouldPrevResult = new Uri("bb", UriKind.RelativeOrAbsolute);
                    break;
                case 3:
                    usermedia = new List<Uri>()
                    {
                        new Uri("aa", UriKind.RelativeOrAbsolute),
                        new Uri("bb", UriKind.RelativeOrAbsolute),
                        new Uri("cc", UriKind.RelativeOrAbsolute)
                    };
                    shouldNextResult = new Uri("aa", UriKind.RelativeOrAbsolute);
                    shouldPrevResult = new Uri("cc", UriKind.RelativeOrAbsolute);
                    break;
            }
            const int userID = 1;
            Account.UserID = userID;
            Profile profile = ProfileDataAccess.LoadProfile(userID);
            profile.UserMedia = usermedia;
            ProfilePageViewModel viewModel = new ProfilePageViewModel(profile);

            void NextImage()
            {
                viewModel.PhotoNavigationButtonCommand.Execute("+");
            }

            void PrevImage()
            {
                viewModel.PhotoNavigationButtonCommand.Execute("-");
            }

            Assert.DoesNotThrow(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    NextImage();
                }
            });
            Assert.AreEqual(shouldNextResult, viewModel.SelectedImage);

            Assert.DoesNotThrow(() =>
            {
                for (int i = 0; i < 4; i++)
                {
                    PrevImage();
                }
            });
            Assert.AreEqual(shouldPrevResult, viewModel.SelectedImage);
        }
    }
}
