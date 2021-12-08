using NUnit.Framework;
using System;
using System.IO;
using System.Net;

namespace Gateway.Test
{
    public class Gateway_MediaDataAccess_UploadMediaToServer_and_DeleteMediaFromServer
    {
        [Test]
        public void UploadMediaToServer_and_DeleteMediaFromServer_ExistingFile_FileIsAccessibleOnWebhostAndDeletedAfterwards()
        {
            // Arrange
            const string filename = "Gateway.dll"; // Pick a random file present on every debug run
            string file = Directory.GetCurrentDirectory() + @"\" + filename;
            long lengthOfFile = new System.IO.FileInfo(file).Length; // Get the length in bytes of that file

            // Act
            void UploadTestFile()
            {
                try
                {
                    MediaDataAccess.UploadMediaToServer(file);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            long TryDownloadFile()
            {
                try
                {
                    // Attempt to see if the file exists on the remote server with WebRequest
                    WebRequest request = WebRequest.Create(new Uri("http://www.stugether.wafoe.nl/media/" + filename));
                    request.Method = "HEAD";

                    using (WebResponse response = request.GetResponse())
                    {
                        return response.ContentLength;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            void DeleteTestFile()
            {
                try
                {
                    MediaDataAccess.DeleteMediaFromServer(filename);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
            // Assert
            Assert.DoesNotThrow(() => UploadTestFile(), "Uploading the test file");
            Assert.DoesNotThrow(() => TryDownloadFile(), "Downloading the test file");
            Assert.AreEqual(lengthOfFile, TryDownloadFile(), "Comparing length in bytes of file");
            Assert.DoesNotThrow(() => DeleteTestFile(), "Deleting the test file");
        }
    }
}