using System;

public interface IAuthHandler
{
    public delegate void OnCredentialsAuthorisedDel();

    void Login();
    bool IsAuthorised();
    void RegisterAuthorisationCallback(Action InAction);

    void SetUserPhoto(System.Windows.Controls.Image InImage);
    void UploadFileToDrive(string FileDir);
}
