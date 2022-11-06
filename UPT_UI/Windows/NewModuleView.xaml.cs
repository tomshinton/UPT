using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UPT_UI.Windows
{
    public partial class NewModuleView : Window
    {
        public NewModuleView()
        {
            InitializeComponent();

            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            ValidateModuleData();
        }

        private void NewModuleNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ErrorString != null)
            {
                ValidateModuleData();
            }
        }

        private void LoadPhaseBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateModuleData();
        }

        private void ModuleTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateModuleData();
        }

        private void ValidateModuleData()
        {
            bool IsValid = true;
            ErrorString.Visibility = IsValid ? Visibility.Hidden : Visibility.Visible;

            string ModuleName = NewModuleNameBox.Text;

            string LoadPhase = "";
            if(LoadPhaseBox.SelectedIndex != -1)
            {
                LoadPhase = (LoadPhaseBox.SelectedItem as ComboBoxItem).Content.ToString();
            }

            string Type = "";
            if (ModuleTypeBox.SelectedIndex != -1)
            {
                Type = (ModuleTypeBox.SelectedItem as ComboBoxItem).Content.ToString();
            }

            if (ModuleName.Contains(" "))
            {
                ErrorString.Text = "Module cannot contain spaces";
                IsValid = false;
            }

            if (ModuleName == "")
            {
                ErrorString.Text = "Cannot add a module with no name";
                IsValid = false;
            }

            if(ModuleName == EmptyModuleToken)
            {
                ErrorString.Text = EmptyModuleToken + " is reserved - cannot name module " + EmptyModuleToken;
                IsValid = false;
            }

            foreach (ModuleData Module in MainWindow.ProjectWorker.Proxy.Modules)
            {
                if (Module.Name == ModuleName)
                {
                    ErrorString.Text = "Module " + ModuleName + " already existing in project";
                    IsValid = false;
                }

                if (LoadPhase == "None" || LoadPhase == "")
                {
                    ErrorString.Text = "Invalid LoadPhase (" + LoadPhase + ") selected";
                    IsValid = false;
                }

                if (Type == "None" || Type == "")
                {
                    ErrorString.Text = "Invalid ModuleType (" + Type + ") selected";
                    IsValid = false;
                }

            }

            AddModuleButton.IsEnabled = IsValid;
            ErrorString.Visibility = IsValid ? Visibility.Hidden : Visibility.Visible;
        }

        private void AddModuleButton_Click(object sender, RoutedEventArgs e)
        {
            OnNewModuleCreated(new ModuleData(NewModuleNameBox.Text, (ModuleTypeBox.SelectedItem as ComboBoxItem).Content.ToString(), (LoadPhaseBox.SelectedItem as ComboBoxItem).Content.ToString()));
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnNewModuleCreated(ModuleData InNewModuleData)
        {
            MainWindow.ProjectWorker.AddModuleToProxy(InNewModuleData);

            CLDesc = "[UPT] Create " + InNewModuleData.Name + " module";
            MainWindow.Instance.BuildModulePanel();

            string TempZipDir = System.IO.Path.GetTempPath();

            if (Directory.Exists(System.IO.Path.Combine(TempZipDir, EmptyModuleToken)))
            {
                Directory.Delete(System.IO.Path.Combine(TempZipDir, EmptyModuleToken), true);
            }

            ZipFile.ExtractToDirectory(MainWindow.ProjectWorker.EmptyModuleFiles, TempZipDir);

            string TempRenamedDir = System.IO.Path.Combine(TempZipDir, InNewModuleData.Name);
            if (Directory.Exists(TempRenamedDir))
            {
                Directory.Delete(TempRenamedDir, true);
            }

            Directory.Move(System.IO.Path.Combine(TempZipDir, EmptyModuleToken), (System.IO.Path.Combine(TempZipDir, InNewModuleData.Name)));

            string NewDirectoryName = System.IO.Path.Combine(MainWindow.ProjectWorker.SourceDirectory, InNewModuleData.Type, InNewModuleData.Name);

            if(!Directory.Exists(NewDirectoryName))
            {
                Directory.CreateDirectory(NewDirectoryName);
            }

            string[] Files = Directory.GetFiles(TempRenamedDir);
            foreach (string CurrFile in Files)
            {
                string text = File.ReadAllText(CurrFile);
                text = text.Replace(EmptyModuleToken, InNewModuleData.Name);
                File.WriteAllText(CurrFile, text);

                string NewName = CurrFile.Replace(EmptyModuleToken, InNewModuleData.Name);
                System.IO.File.Move(CurrFile, NewName);
            }

            MainWindow.SourceControlHandler.CheckoutFiles(CopyFolder(TempRenamedDir, NewDirectoryName), CLDesc);
            
            if (InNewModuleData.Type == "Runtime")
            {
                string[] PrimaryBuildFileContents = File.ReadAllLines(MainWindow.ProjectWorker.PrimaryGameplayBuildFile);
                List<string> ContentsAsList = new List<string>();
                ContentsAsList = PrimaryBuildFileContents.OfType<string>().ToList();

                //Existing PrivateDependancy block
                int InjectionIndex = ContentsAsList.FindIndex(
                delegate (string Line)
                {
                    return Line.Contains(@"PrivateDependencyModuleNames");
                });

                if (InjectionIndex > -1)
                {
                    if (ContentsAsList[InjectionIndex + 1].Contains(@"{"))
                    {
                        InjectionIndex++;
                    }

                    //Add one to add the line after the injection index
                    ContentsAsList.Insert(InjectionIndex + 1, '\u0022' + InNewModuleData.Name + '\u0022' + ",");
                }
                else
                {
                    int StartOfConstructorLine = ContentsAsList.FindIndex(
                    delegate (string Line)
                    {
                        return Line.Contains(@"public " + MainWindow.ProjectWorker.PrimaryModuleName);
                    });

                    if (StartOfConstructorLine > -1)
                    {
                        if (ContentsAsList[StartOfConstructorLine + 1].Contains(@"{"))
                        {
                            StartOfConstructorLine++;
                        }

                        ContentsAsList.Insert(StartOfConstructorLine + 1, @"PrivateDependencyModuleNames.AddRange(new string[]");
                        ContentsAsList.Insert(StartOfConstructorLine + 2, @"{");
                        ContentsAsList.Insert(StartOfConstructorLine + 3, '\u0022' + InNewModuleData.Name + '\u0022' + ",");
                        ContentsAsList.Insert(StartOfConstructorLine + 4, @"});");
                    }
                }

                if(MainWindow.SourceControlHandler.CheckoutFile(MainWindow.ProjectWorker.PrimaryGameplayBuildFile, CLDesc))
                {
                    File.WriteAllLines(MainWindow.ProjectWorker.PrimaryGameplayBuildFile, ContentsAsList.ToArray());
                }
            }

            MainWindow.SourceControlHandler.CheckoutFile(MainWindow.ProjectWorker.ProjectFile, CLDesc);
            MainWindow.ProjectWorker.Save();
        }

        public List<string> CopyFolder(string InSourceFolder, string InDestFolder)
        {
            List<string> NewLocations = new List<string>();

            if (!Directory.Exists(InDestFolder))
            {
                Directory.CreateDirectory(InDestFolder);
            }

            string[] Files = Directory.GetFiles(InSourceFolder);
            foreach (string FoundFile in Files)
            {
                string FileName = Path.GetFileName(FoundFile);
                string DestinationFile = Path.Combine(InDestFolder, FileName);

                File.Copy(FoundFile, DestinationFile);

                NewLocations.Add(DestinationFile);
            }

            string[] Folders = Directory.GetDirectories(InSourceFolder);
            foreach (string Folder in Folders)
            {
                string FolderName = Path.GetFileName(Folder);
                string Destination = Path.Combine(InDestFolder, FolderName);
                CopyFolder(Folder, Destination);
            }

            return NewLocations;
        }

        static string EmptyModuleToken = @"Empty";
        string CLDesc = "";
    }
}
