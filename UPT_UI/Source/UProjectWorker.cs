using Google.Apis.PeopleService.v1.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

public enum TargetType
{
    Client,
    Server
}

namespace UPT_UI
{
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
    }

    public class DeployQueueEntry
    {
        public DeployQueueEntry(TargetType InTargetType, string InDeployString, string InDeployDir)
        {
            Type = InTargetType;
            DeployString = InDeployString;
            DeployDir = InDeployDir;
        }

        public TargetType Type;

        public string DeployString;
        public string DeployDir;

        public override bool Equals(object obj)
        {
            return (TargetType)obj == Type;
        }
    }

    public class UProjectWorker
    {
        public delegate void OnProjectInitialised();
        public OnProjectInitialised OnProjectInitialisedDel;

        public UProjectWorker(string InProjectFile, string InEngineDirectory)
        {
            MainWindow.ClearProgress();

            SyncContext = SynchronizationContext.Current;

            Task.Factory.StartNew(() =>
            {
                return AsyncFindBatchFiles(InEngineDirectory);
            })
            .Unwrap()
            .ContinueWith(cacheEngineBatchFiles =>
            {
                if(cacheEngineBatchFiles.Result)
                {
                    ProjectFile = InProjectFile;
                    EngineDirectory = InEngineDirectory;

                    GenerateProjectProxyObject();

                    DefaultGameConfigReader = new IniReader(CacheDefaultGamePath());

                    CacheSourcePath();
                    CachePrimaryGameplayBuildFile();

                    CacheBuildDirectories();

                    CacheTargets();

                    Properties.Settings.Default.LastProjectDir = InProjectFile;
                    Properties.Settings.Default.LastEngineDir = InEngineDirectory;

                    OnProjectInitialisedDel();
                }
                else
                {
                    MessageBox.Show("Failed to find either Unreal Automation Tool, or a built version of the Editor", "Cannot bind to Unreal Engine", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        ~UProjectWorker()
        {
            if(CurrentDeployProcess != null)
            {
                CurrentDeployProcess.Kill();
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
            if (MainWindow.SourceControlHandler.IsInitialised())
            {
                MainWindow.SourceControlHandler.CheckoutFile(ProjectFile);
            }

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
                UpdateLog("Found dir " + Dir);
                if (Dir.Contains(@"Config"))
                {
                    UpdateLog("Potential config folder found, checking for Default inis");

                    string[] ConfigFiles = Directory.GetFiles(Dir);

                    foreach (string ConfigFile in ConfigFiles)
                    {
                        if (ConfigFile.Contains(@"DefaultGame"))
                        {
                            UpdateLog("Found DefaultGame.ini, caching and bailing out");
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

        private void CacheBuildDirectories()
        {
            string RootDir = Path.GetDirectoryName(ProjectFile);

            ClientDeployDirectory = RootDir + "\\Saved\\StagedBuilds\\WindowsNoEditor";
            ServerDeployDirectory = RootDir + "\\Saved\\StagedBuilds\\WindowsServer";
        }

        private void CacheTargets()
        {
            string[] allTargetFiles = Directory.GetFiles(SourceDirectory, "*.target.cs", SearchOption.AllDirectories);
            foreach (string targetFile in allTargetFiles)
            {
                UpdateLog("Found " + targetFile + " target in project");

                if (targetFile.Contains("Client"))
                {
                    ClientTargetFile = targetFile;
                }
                else if (targetFile.Contains("Server"))
                {
                    ServerTargetFile = targetFile;
                }

                if (ServerTargetFile != "" && ClientTargetFile != "")
                {
                    return;
                }
            }

            //We're not building a client/server model, so setup for standalone
            if (ServerTargetFile == "" && ClientTargetFile == "")
            {
                UpdateLog("Setting up for standalone game");

                foreach (string targetFile in allTargetFiles)
                {
                    if (targetFile.Contains("Editor"))
                    {
                        continue;
                    }

                    ClientTargetFile = targetFile;
                    return;
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
            PrimaryGameplayBuildFile = Path.Combine(SourceDirectory, CurrentPrimaryModuleName, CurrentPrimaryModuleName) + ".build.cs";
            PrimaryModuleName = CurrentPrimaryModuleName;
        }

        public string GetProjectName()
        {
            return DefaultGameConfigReader.GetValForKey(@"ProjectName").Replace(" ", "");
        }

        public void SetUAT(string InUAT)
        {
            AssociatedUAT = InUAT;
        }

        public void SetEditor(string InEditor)
        {
            AssociatedEditor = InEditor;
        }

        public void DeployServer()
        {
            if(DeployQueue.Find(x => {  return x.Type == TargetType.Server; }) != null)
            {
                return;
            }

            string ServerDeployString =
                "/C " +
                 AssociatedUAT +
                 " -ScriptsForProject=" + ProjectFile +
                 " BuildCookRun -project=" + ProjectFile +
                 " -noP4 " +
                 " -clientconfig=DebugGame" +
                 " -serverconfig=DebugGame " +
                 " -nocompileeditor " +
                 " -ue4exe=" + AssociatedEditor +
                 " -utf8output " +
                 " -server " +
                 " -serverplatform=Win64 " +
                 " -noclient " +
                 " -build " +
                 " -cook " +
                 " -unversionedcookedcontent " +
                 " -compressed " +
                 " -stage " +
                 " -deploy " +
                 " -compile";

            DeployQueue.Add(new DeployQueueEntry(TargetType.Server, ServerDeployString, ServerDeployDirectory));
        }

        public void DeployClient()
        {
            if (DeployQueue.Find(x => { return x.Type == TargetType.Client; }) != null)
            {
                return;
            }

            string ClientDeployString =
                "/C " +
                AssociatedUAT +
                " BuildCookRun -project=" + ProjectFile +
                " -noP4 " +
                " -clientconfig=DebugGame" +
                " -serverconfig=DebugGame " +
                " -nocompileeditor " +
                " -ue4exe=" + AssociatedEditor +
                " -Win64" +
                " -Shipping" +
                " -utf8output" +
                " -platform=Win64" +
                " -targetplatform=Win64 " +
                " -build " +
                " -cook " +
                " -unversionedcookedcontent " +
                " -compressed " +
                " -stage " +
                " -deploy " +
                " -compile";

            DeployQueue.Add(new DeployQueueEntry(TargetType.Client, ClientDeployString, ClientDeployDirectory));
        }

        public void DoDeploy(string InDeployString, string InTargetPlatform)
        {
            if (CurrentDeployProcess != null)
            {
                CurrentDeployProcess.Kill();
            }

            CurrentDeployProcess = new System.Diagnostics.Process();

            System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
            StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            StartInfo.FileName = "CMD.exe";
            StartInfo.Arguments = InDeployString;
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
            
            StartInfo.CreateNoWindow = true;

            CurrentDeployProcess.StartInfo = StartInfo;

            CurrentDeployProcess.OutputDataReceived += (sender, args) => UpdateLog(args.Data);
            CurrentDeployProcess.ErrorDataReceived += (sender, args) => UpdateLog(args.Data);

            CurrentDeployProcess.EnableRaisingEvents = true;
            CurrentDeployProcess.Exited += (sender, args) => OnDeployCompleted(InTargetPlatform);

            CurrentDeployProcess.Start();
            CurrentDeployProcess.BeginOutputReadLine();
            CurrentDeployProcess.BeginErrorReadLine();
        }

        private static void UpdateLog(string InOutput)
        {
            SyncContext.Post(_ => MainWindow.ReportProgress(InOutput), null);
        }

        private void OnDeployCompleted(string InDeployFolder)
        {
            SyncContext.Post(_ => OnDeployCompletedImpl(InDeployFolder), null);
        }

        private void OnDeployCompletedImpl(string InDeployFolder)
        {
            MainWindow.ReportProgress("Deploy completed - pushing staged build to appropriate locations");

            if(!MainWindow.CurrAuthHandler.IsAuthorised())
            {
                if(MessageBox.Show("You are not currently connected to Cloud Services.  Would you like to connect now and push your new build to the cloud?", "Cloud Services", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MainWindow.CurrAuthHandler.Login();
                }
            }

            if(MainWindow.CurrAuthHandler.IsAuthorised())
            {
                MainWindow.ReportProgress("---> Connected to Cloud Services <---");

                string ZipDir = InDeployFolder + "_Release.zip";
                MainWindow.ReportProgress("Creating Zip");

                if(File.Exists(ZipDir))
                {
                    MainWindow.ReportProgress("Zip already exists at location - nuking in place of new build");
                    File.Delete(ZipDir);
                }

                ZipFile.CreateFromDirectory(InDeployFolder, ZipDir);
                MainWindow.ReportProgress("Build zipped as " + ZipDir);
                MainWindow.ReportProgress("Attempting to push file to Drive");

                Task.Factory.StartNew(() =>
                {
                    MainWindow.CurrAuthHandler.UploadFileToDrive(ZipDir);
                });               
            }

            ProcessDeployQueue();
        }

        private static async Task<bool> AsyncFindBatchFiles(string InEngineDirectory)
        {
            return await Task.Run(async () =>
            {
                string[] allBatchFiles = Directory.GetFiles(InEngineDirectory, "*.bat", SearchOption.AllDirectories);

                foreach (string foundBatch in allBatchFiles)
                {
                    //1ms delay to account for threadsync
                    await Task.Delay(1);
                    UpdateLog("Found " + foundBatch + " in Engine Dir");

                    if (foundBatch.Contains(UATSubstring))
                    {
                        AssociatedUAT = foundBatch;
                        UpdateLog("Setting " + foundBatch + " as associated Unreal Automation Tool");

                        break;
                    }
                }

                string[] allExeFiles = Directory.GetFiles(InEngineDirectory, "*.exe", SearchOption.AllDirectories);

                foreach (string foundExe in allExeFiles)
                {
                    //1ms delay to account for threadsync
                    await Task.Delay(1);
                    UpdateLog("Found " + foundExe + " in Engine Dir");

                    if (foundExe.Contains(EditorSubstring))
                    {
                        AssociatedEditor = foundExe;
                        UpdateLog("Setting " + foundExe + " as associated Editor build");

                        break;
                    }
                }

                return (AssociatedEditor != null) && AssociatedUAT != null;
            });
        }

        public bool SupportsTarget(TargetType InTargetType)
        {
            switch (InTargetType)
            {
                case TargetType.Client: 
                    return ClientTargetFile != "";
                case TargetType.Server:
                    return ServerTargetFile != "";   
            }

            return false;
        }

        public void ClearDeployQueue()
        {
            DeployQueue.Clear();
        }

        public void ProcessDeployQueue()
        {
            if(DeployQueue.Count > 0)
            {
                DeployQueueEntry Entry = DeployQueue[0];
                DoDeploy(Entry.DeployString, Entry.DeployDir);
                DeployQueue.RemoveAt(0);
            }
        }

        public UProjectProxy Proxy;

        public string ProjectFile;
        private string EngineDirectory;

        public IniReader DefaultGameConfigReader;

        public string DefaultGameConfig = "";
        public string SourceDirectory = "";
        public string ClientDeployDirectory = "";
        public string ServerDeployDirectory = "";
        public string PrimaryGameplayBuildFile = "";
        public string EmptyModuleFiles = GetEmptyModulePath();
        public string PrimaryModuleName = "";

        public string ClientTargetFile = "";
        public string ServerTargetFile = "";

        static public string DependencyInjectionString = "//AutoGenerated from UPT - https://github.com/tomshinton/UPT";

        static string UATSubstring = "UAT";
        static string EditorSubstring = "UE4Editor-Cmd";

        private static string AssociatedUAT;
        private static string AssociatedEditor;

        static SynchronizationContext SyncContext;
        System.Diagnostics.Process CurrentDeployProcess;

        List<DeployQueueEntry> DeployQueue = new List<DeployQueueEntry>();
    }
}
