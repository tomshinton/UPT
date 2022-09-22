using System.Collections.Generic;

public interface ISourceControlHandler
{
    void Connect(string InURL, string InUser, string InWorkspace, string InPassword = "");

    bool CheckoutFile(string InFile, string InCLName = "")
    {
        List<string> NewList = new List<string>() { InFile };
        return CheckoutFiles(NewList, InCLName);
    }

    bool CheckoutFiles(List<string> InFiles, string InCLName = "");

    bool IsInitialised();
}
