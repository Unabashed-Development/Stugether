using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ViewModel.Test
{
    public class ViewModel_ProfilePagePhotosViewModel_AddDelete
    {
        [Test]
        public void ProfilePagePhotosViewModel_AddDelete()
        {
            Account.UserID = 1;
            string fileToUpload = Directory.GetCurrentDirectory() + @"\ViewModel_ProfilePagePhotosViewModel_Files\Perfect.bin";
            ProfilePagePhotosViewModel viewModel = new ProfilePagePhotosViewModel();

            int imagesCount = viewModel.Images.Count;

            viewModel.SelectedMediaFileForUpload = fileToUpload;
            Assert.DoesNotThrow(() =>
            {
                viewModel.UploadPhotoButtonCommand.Execute(null);
            }, "Upload file");

            Assert.AreEqual(imagesCount + 1, viewModel.Images.Count, "File is uploaded");

            Uri fileToRemove;
            Assert.DoesNotThrow(() =>
            {
                fileToRemove = new List<Uri>(viewModel.Images).FindLast((u) =>  u.ToString().Contains(".bin"));
                viewModel.DeletePhotoButtonCommand.Execute(fileToRemove);
                Assert.IsFalse(new List<Uri>(viewModel.Images).Contains(fileToRemove), "File is removed");
            }, "Delete file");

        }
    }
}
