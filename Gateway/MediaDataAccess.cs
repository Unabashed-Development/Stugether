using Dapper;
using Gateway.Services;
using System.Collections.Generic;
using System.Data;
using WinSCP;

namespace Gateway
{
    public static class MediaDataAccess
    {
        public static IEnumerable<string> GetUserMedia(int userID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                return connection.Query<string>("SELECT Path FROM UserMedia WHERE UserID = @uid", new { uid = userID });
            }
        }

        /// <summary>
        /// Uploads a local file to the media server.
        /// </summary>
        /// <param name="localPath">The path of the media that needs to be uploaded.</param>
        public static void UploadMediaToServer(string localPath)
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(SFTPService.GenerateSessionOptions());

                session.PutFiles($"{localPath}", $"/mnt/StorageDisk/stugether/media/").Check();

                session.Close();
            }
        }

        /// <summary>
        /// Deletes a remote file from the media server.
        /// </summary>
        /// <param name="remoteFile">The name of the file that needs to be deleted.</param>
        public static void DeleteMediaFromServer(string remoteFile)
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(SFTPService.GenerateSessionOptions());

                session.RemoveFile($"/mnt/StorageDisk/stugether/media/{remoteFile}");

                session.Close();
            }
        }
    }
}
