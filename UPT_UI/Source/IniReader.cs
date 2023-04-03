using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UPT_UI;

public class IniReader
{
    public IniReader(string InPath)
    {
        CachedIniPath = InPath;
        Debug.WriteLine("Initialising new Ini Reader for " + Path.GetFileName(CachedIniPath));
        Parse();
    }

    public string GetValForKey(string InKey)
    {
        if (Pairs.ContainsKey(InKey))
        {
            return Pairs[InKey];
        }

        return "";
    }

    private void Parse()
    {
        string[] Lines = System.IO.File.ReadAllLines(CachedIniPath);

        foreach (string Line in Lines)
        {
            string[] SplitLine = Line.Split(DelimitingChar.ToCharArray());

            if (SplitLine.Length == 2)
            {
                try
                {
                    Pairs.Add(SplitLine[0], SplitLine[1]);
                }
                catch (Exception e)
                {
                    MainWindow.ReportProgress(e.Message);
                }
            }
        }
    }

    string CachedIniPath;
    Dictionary<string, string> Pairs = new Dictionary<string, string>();

    string DelimitingChar = @"=";
}