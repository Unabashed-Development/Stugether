using Dapper;
using Gateway.Services;
using System.Collections.Generic;
using System.Data;
using WinSCP;

namespace Gateway
{
    public static class MediaDataAccess
    {
        #region SQL
        /// <summary>
        /// Gets all media for userID from the database
        /// </summary>
        /// <param name="userID">The userID to get the media for</param>
        /// <returns>IEnumerable with paths to the media</returns>
        public static IEnumerable<string> GetUserMedia(int userID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                return connection.Query<string>("SELECT Path FROM UserMedia WHERE UserID = @uid", new { uid = userID });
            }
        }

        /// <summary>
        /// Uploads media to the server and adds its record to the database
        /// </summary>
        /// <param name="localPath">The local path to the file to upload on the computer</param>
        /// <param name="userID">The userID to upload the media for</param>
        public static void AddUserMedia(string localPath, int userID)
        {
            string uploadedFile = UploadMediaToServer(localPath);
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Query("INSERT INTO UserMedia (UserID, Path) VALUES(@uid, @file); ", new { uid = userID, file = $"http://www.stugether.wafoe.nl/{uploadedFile}" });
            }
        }

        /// <summary>
        /// Deletes media from the server and removes its record to the database
        /// </summary>
        /// <param name="remotePath">The remote path where the media can be found</param>
        /// <param name="userID">The userID from who the media is</param>
        public static void DeleteUserMedia(string remotePath, int userID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                List<dynamic> doesExist = connection.Query("SELECT UserID, Path FROM UserMedia WHERE UserID = @uid AND Path = @path", new { uid = userID, path = remotePath }).AsList();
                if (doesExist.Count != 1) return;
                var row = doesExist[0];
                if (row.UserID != userID || row.Path != remotePath) return;
                
                if (remotePath.Contains("http://www.stugether.wafoe.nl/media/"))
                {
                    string relativePath = remotePath.Substring("http://www.stugether.wafoe.nl/media/".Length);

                    DeleteMediaFromServer(relativePath);
                }

                connection.Query("DELETE FROM UserMedia WHERE UserID = @uid AND Path = @path", new { uid = userID, path = remotePath });
            }
        }
        #endregion

        #region SFTP
        /// <summary>
        /// Uploads a local file to the media server.
        /// </summary>
        /// <param name="localPath">The path of the media that needs to be uploaded.</param>
        /// <returns>The relative path where the media is stored.</returns>
        public static string UploadMediaToServer(string localPath)
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(SFTPService.GenerateSessionOptions());

                // Generate random filename for remote
                string filename = "";
                do
                {
                    System.Guid filenameGuid = System.Guid.NewGuid();
                    filename = filenameGuid.ToString() + new System.IO.FileInfo(localPath).Extension;
                    System.Diagnostics.Debug.WriteLine(filename);
                }
                while (session.FileExists($"/mnt/StorageDisk/stugether/media/{filename}"));

                session.PutFiles($"{localPath}", $"/mnt/StorageDisk/stugether/media/{filename}").Check();

                session.Close();

                return $"media/{filename}";
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
        #endregion
    }
}
