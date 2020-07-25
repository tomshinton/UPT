using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Parse();
        }

        public void AddModuleToProxy(ModuleData InNewModule)
        {
            Proxy.Modules.Add(InNewModule);
        }
        private void Parse()
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

        public UProjectProxy Proxy;
        private string ProjectFile;
    }
}
