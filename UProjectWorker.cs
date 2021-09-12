using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using System.Windows.Forms;
using Perforce.P4;
using UnrealProjectTool.Properties;

namespace UnrealProjectTool
{
    public class PluginData
    {
        public PluginData(string InName, bool InEnabled)
        {
            Name = InName;
            Enabled = InEnabled;
        }

        public string Name { get; set; }
        public bool Enabled { get; set; }

        public List<string> SupportedTargetPlatforms { get; set; }
    }

    public class ModuleData
    {
        public ModuleData(string InName, string InType, string InLoadingPhase)
        {
            Name = InName;
            Type = InType;
            LoadingPhase = InLoadingPhase;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string LoadingPhase { get; set; }
        public string[] AdditionalDependencies { get; set; }
    }

    public class UProjectProxy
    {
        [JsonProperty(Order = 1)]
        public string FileVersion { get; set; }
        [JsonProperty(Order = 2)]
        public string EngineAssociation { get; set; }
        [JsonProperty(Order = 3)]
        public string Category { get; set; }
        [JsonProperty(Order = 4)]
        public string Description { get; set; }
        [JsonProperty(Order = 5)]
        public List<ModuleData> Modules = new List<ModuleData>();
        [JsonProperty(Order = 6)]
        public List<PluginData> Plugins { get; set; }
    }

    class UProjectWorker
    {
        public delegate void OnProjectInitialiserDelegate();
        public OnProjectInitialiserDelegate OnProjectInitialised;
        public UProjectWorker(string InProjectFile)
        {
            ProjectFile = InProjectFile;
        }
        public void Initialise()
        {
            //If the file is read only, it is likely under source control - try and connect to P4, if we can
            System.IO.FileInfo ProjectInfo = new System.IO.FileInfo(@ProjectFile);
            DialogResult ReadOnlyResult = DialogResult.None;
            if (ProjectInfo.IsReadOnly && ConnectedRepo == null)
            {
                ReadOnlyResult = MessageBox.Show("The file " + ProjectFile + " is read only - module injection will fail - would you like to connect to Perforce?", "File is Read-Only", MessageBoxButtons.YesNoCancel);
            }

            if (ReadOnlyResult == DialogResult.No || ReadOnlyResult == DialogResult.None)
            {
                InitialiseProjectWorker();
            }
            else if (ReadOnlyResult == DialogResult.Yes)
            {
                P4Connection NewConnectionWindow = new P4Connection();

                //bind to overloaded ProjectWorker, caching connection
                NewConnectionWindow.OnP4Connected = InitialiseProjectWorker;

                if(!NewConnectionWindow.TryConnectFromPersistance() && NewConnectionWindow != null)
                {
                    try
                    {
                        NewConnectionWindow.Show();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
        public void AddModuleToProxy(ModuleData InNewModule)
        {
            Proxy.Modules.Add(InNewModule);
        }
        private void GenerateProjectProxyObject()
        {
            using (StreamReader r = new StreamReader(ProjectFile))
            {
                string json = r.ReadToEnd();
                Proxy = JsonConvert.DeserializeObject<UProjectProxy>(json);
            }
        }

        public void Save()
        {
            string SerializedObject = JsonConvert.SerializeObject(Proxy, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            System.IO.File.WriteAllText(ProjectFile, SerializedObject);
        }

        private string CacheDefaultGamePath()
        {
            string[] Dirs = Directory.GetDirectories(Path.GetDirectoryName(ProjectFile));

            foreach (string Dir in Dirs)
            {
                Debug.WriteLine("Found dir " + Dir);
                if (Dir.Contains(@"Config"))
                {
                    Debug.WriteLine("Potential config folder found, checking for Default inis");

                    string[] ConfigFiles = Directory.GetFiles(Dir);

                    foreach (string ConfigFile in ConfigFiles)
                    {
                        if (ConfigFile.Contains(@"DefaultGame"))
                        {
                            Debug.WriteLine("Found DefaultGame.ini, caching and bailing out");
                            DefaultGameConfig = ConfigFile;
                            break;
                        }
                    }

                    if (DefaultGameConfig != "")
                    {
                        break;
                    }
                }
            }

            return DefaultGameConfig;
        }

        private void CacheSourcePath()
        {
            string[] Dirs = Directory.GetDirectories(Path.GetDirectoryName(ProjectFile));

            foreach (string Dir in Dirs)
            {
                if (Dir.Contains(@"Source"))
                {
                    SourceDirectory = Dir;
                    break;
                }
            }
        }

        static private string GetEmptyModulePath()
        {
            string AppPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            string FilePath = Path.Combine(AppPath, "Resources");
            return Path.Combine(FilePath, "EmptyModuleTemplate.zip");
        }

        private void CachePrimaryGameplayBuildFile()
        {
            string CurrentPrimaryModuleName = DefaultGameConfigReader.GetValForKey(@"ProjectName").Replace(" ", "");
            PrimaryGameplayBuildFile = Path.Combine(SourceDirectory, CurrentPrimaryModuleName, CurrentPrimaryModuleName) + ".Build.cs";
            PrimaryModuleName = CurrentPrimaryModuleName;
        }

        public bool IsValid()
        {
            return ProjectFile != "" && Proxy != null;
        }

        public void InitialiseProjectWorker(Perforce.P4.Repository InRepo)
        {
            ConnectedRepo = InRepo;

            InitialiseProjectWorker();
        }
        public void InitialiseProjectWorker()
        {
            GenerateProjectProxyObject();

            DefaultGameConfigReader = new IniReader(CacheDefaultGamePath());

            CacheSourcePath();
            CachePrimaryGameplayBuildFile();

            OnProjectInitialised.Invoke();
        }

        public string RepoAsString()
        {
            if(ConnectedRepo == null)
            {
                return "";
            }

            string RepoAsString = "    - [Perforce] " 
                      + ConnectedRepo.Connection.UserName + ", "
                      + ConnectedRepo.Connection.Server.Address.ToString();

            return RepoAsString;
        }

        public UProjectProxy Proxy;
        public string ProjectFile;

        public IniReader DefaultGameConfigReader;

        public string DefaultGameConfig = "";
        public string SourceDirectory = "";
        public string PrimaryGameplayBuildFile = "";
        public string EmptyModuleFiles = GetEmptyModulePath();
        public string PrimaryModuleName = "";

        static public string DependencyInjectionString = "//AutoGenerated from UPT - https://github.com/tomshinton/UPT";

        public Repository ConnectedRepo;
    }
}
