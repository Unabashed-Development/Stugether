using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gateway.Test
{
    public class Gateway_MediaDataAccess_AddAndDeleteMedia
    {
        [Test]
        public void AddAndDeleteMedia()
        {
            const int UserID = 1;
            string filename = "Gateway.dll";
            string file = Directory.GetCurrentDirectory() + @"\" + filename;

            void AddMedia()
            {
                try
                {
                    MediaDataAccess.AddUserMedia(file, UserID);
                }
                catch
                {
                    throw;
                }
            }

            void GetMedia()
            {
                try
                {
                    List<string> medias = new List<string>(MediaDataAccess.GetUserMedia(UserID));
                    List<Uri> mediaUris = new List<Uri>(MediaDataAccess.GetUserMediaUris(UserID));

                    Assert.IsFalse(medias.Count == 0, "No media has been fetched, list is empty");
                    file = medias[0];

                    foreach(string media in medias)
                    {
                        Uri mediauri = new Uri(media, UriKind.RelativeOrAbsolute);
                        Assert.IsTrue(mediaUris.Contains(mediauri), $"Media are not equal: {media}");
                    }
                }
                catch
                {
                    throw;
                }
            }

            void DeleteMedia()
            {
                try
                {
                    MediaDataAccess.DeleteUserMedia(file, UserID);
                }
                catch
                {
                    throw;
                }
            }

            void DeleteInvalidName()
            {
                try
                {
                    MediaDataAccess.DeleteUserMedia("aaa", UserID);
                }
                catch
                {
                    throw;
                }
            }

            Assert.DoesNotThrow(() => AddMedia(), "Add media");
            Assert.DoesNotThrow(() => GetMedia(), "Fetching media");
            Assert.DoesNotThrow(() => DeleteMedia(), "Deleting media");
            Assert.DoesNotThrow(() => DeleteInvalidName(), "Delete invalid media");
        }
    }
}
