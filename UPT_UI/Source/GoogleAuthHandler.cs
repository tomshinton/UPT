using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;
using System.Threading;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.IO;
using Google.Apis.Drive.v3;

namespace UPT_UI
{
    public class GoogleAuthHandler : IAuthHandler
    {
        private const string ClientID = "149826142245-efjc04o2hmfvlhp7oabj3jstfuslen8a.apps.googleusercontent.com";
        private const string ClientSecret = "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";

        static string[] Scopes = {
                "https://www.googleapis.com/auth/drive",
                "https://www.googleapis.com/auth/userinfo.profile"

        };

        private Person User;

        Action CallbackAction;
        Google.Apis.Auth.OAuth2.UserCredential Credentials;

        public void Login()
        {
            if (User == null)
            {
                ClientSecrets Secrets = new ClientSecrets { ClientId = ClientID, ClientSecret = ClientSecret };
                Credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(Secrets, Scopes, "user", CancellationToken.None).Result;

                if (Credentials.Token.IsExpired(SystemClock.Default))
                {
                    Credentials.RefreshTokenAsync(CancellationToken.None).Wait();
                }

                PeopleServiceService PeopleService = new PeopleServiceService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = Credentials
                });

                var Request = PeopleService.People.Get("people/me");
                Request.PersonFields = "names,Photos";
                User = Request.Execute();

                CallbackAction();
            }
        }

        public bool IsAuthorised()
        {
            return User != null;
        }

        public void RegisterAuthorisationCallback(Action InAction)
        {
            CallbackAction = InAction;
        }

        public void SetUserPhoto(System.Windows.Controls.Image InImage)
        {
            IList<Photo> Photos = User.Photos;

            Photo Photo = Photos[0];
            if (!Photo.Equals(""))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Photo.Url, UriKind.Absolute);
                bitmap.EndInit();

                InImage.Source = bitmap;
            }
        }


        public void UploadFileToDrive(string InFileDir)
        {
            Google.Apis.Drive.v3.DriveService Service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credentials,
            });

            FilesResource.ListRequest ListRequest = Service.Files.List();
            ListRequest.PageSize = 100;
            ListRequest.Fields = "nextPageToken, files(id, name)";

            IList<Google.Apis.Drive.v3.Data.File> Files = ListRequest.Execute().Files;

            bool FolderAlreadyExists = false;
            string FolderID = "";
            if (Files != null && Files.Count > 0)
            {
                foreach (Google.Apis.Drive.v3.Data.File File in Files)
                {
                    if (File.Name == MainWindow.GetProjectName())
                    {
                        MainWindow.ReportProgress("Found TARGET folder [" + MainWindow.GetProjectName() + "] on drive");
                        FolderAlreadyExists = true;
                        FolderID = File.Id;
                        break;
                    }
                }

                foreach (Google.Apis.Drive.v3.Data.File File in Files)
                {
                    if (File.Name == Path.GetFileName(InFileDir).ToString())
                    {
                        Service.Files.Delete(File.Id).Execute();
                    }
                }
            }

            if(!FolderAlreadyExists)
            {
                MainWindow.ReportProgress("Could not find target folder [" + MainWindow.GetProjectName() + "] on drive, creating");

                Google.Apis.Drive.v3.Data.File TargetFile = new Google.Apis.Drive.v3.Data.File { Name = MainWindow.GetProjectName(), MimeType = "application/vnd.google-apps.folder" };

                FilesResource.CreateRequest request = Service.Files.Create(TargetFile);
                request.Fields = "id";
                var file = request.Execute();
                FolderID = file.Id;
            }

            if (FolderID != "")
            {
                Google.Apis.Drive.v3.Data.File BuildFileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(InFileDir).ToString(),
                    Parents = new List<string>
                    {
                        FolderID
                    }
                };

                FilesResource.CreateMediaUpload UploadRequest;
                using (FileStream Stream = new FileStream(InFileDir, FileMode.Open))
                {
                    UploadRequest = Service.Files.Create(BuildFileMetaData, Stream, "Images/Jpeg");
                    UploadRequest.Fields = "id";
                    UploadRequest.Upload();
                }

                if (UploadRequest.ResponseBody.Id != "")
                {
                    MainWindow.ReportProgress("Successfully uploaded new build of " + MainWindow.GetProjectName() + " to Drive");
                }
                else
                {
                    MainWindow.ReportProgress("Upload of " + MainWindow.GetProjectName() + " failed!");
                }
            }
        }
    }
}
