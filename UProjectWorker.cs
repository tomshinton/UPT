﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UnrealProjectTool
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

    class UProjectWorker
    {
        public UProjectWorker(string InProjectFile)
        {
            ProjectFile = InProjectFile;

            GenerateProjectProxyObject();

            DefaultGameConfigReader = new IniReader(CacheDefaultGamePath());

            CacheSourcePath();
            CachePrimaryGameplayBuildFile();
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
            string PrimaryModuleName = DefaultGameConfigReader.GetValForKey(@"ProjectName").Replace(" ", "");
            PrimaryGameplayBuildFile = Path.Combine(SourceDirectory, PrimaryModuleName, PrimaryModuleName) + ".build.cs";
        }

        public UProjectProxy Proxy;
        private string ProjectFile;

        public IniReader DefaultGameConfigReader;

        public string DefaultGameConfig = "";
        public string SourceDirectory = "";
        public string PrimaryGameplayBuildFile = "";
        public string EmptyModuleFiles = GetEmptyModulePath();
    }
}
