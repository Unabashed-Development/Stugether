using Model;
using NUnit.Framework;
using System.IO;

namespace ViewModel.Test
{
    public class ViewModel_ProfilePagePhotosViewModel_SelectFile
    {
        [Test]
        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, false)]
        [TestCase(4, false)]
        [TestCase(5, true)]
        public void ProfilePagePhotosViewModel_SelectFile(int testid, bool expectedResult)
        {
            string selectedfile, expectedStatus;
            switch (testid)
            {
                case 0:
                    selectedfile = null;
                    expectedStatus = "Selecteer een bestand om te uploaden.";
                    break;
                case 1:
                    selectedfile = string.Empty;
                    expectedStatus = "Selecteer een bestand om te uploaden.";
                    break;
                case 2:
                    selectedfile = " ";
                    expectedStatus = "Selecteer een bestand om te uploaden.";
                    break;
                case 3:
                    selectedfile = Directory.GetCurrentDirectory() + @"\ViewModel_ProfilePagePhotosViewModel_Files\NotFound.bin";
                    expectedStatus = "Bestand bestaat niet.";
                    break;
                case 4:
                    selectedfile = Directory.GetCurrentDirectory() + @"\ViewModel_ProfilePagePhotosViewModel_Files\TooBig.bin";
                    Assert.IsTrue(new FileInfo(selectedfile).Exists, "TestFile not found!");
                    expectedStatus = "Bestand is te groot. Het bestand moet kleiner zijn dan 5MB.";
                    break;
                case 5:
                    selectedfile = Directory.GetCurrentDirectory() + @"\ViewModel_ProfilePagePhotosViewModel_Files\Perfect.bin";
                    Assert.IsTrue(new FileInfo(selectedfile).Exists, "TestFile not found!");
                    expectedStatus = "Klaar om te uploaden.";
                    break;
                default:
                    selectedfile = null;
                    expectedStatus = null;
                    Assert.Fail("Test doesn't exist");
                    break;
            }
            const int userID = 1;
            Account.UserID = userID;

            ProfilePagePhotosViewModel viewModel = new ProfilePagePhotosViewModel();
            viewModel.SelectedMediaFileForUpload = selectedfile;

            Assert.AreEqual(viewModel.CanUpload, expectedResult);
            Assert.AreEqual(viewModel.UploadStatus, expectedStatus);
        }
    }
}
