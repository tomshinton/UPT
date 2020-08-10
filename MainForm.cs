using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace UnrealProjectTool
{
    class MainForm : Form
    {
        private TableLayoutPanel MainFormLayoutPanel;
        private Panel InfoPanel;
        private Panel ActionsPanel;
        private GroupBox GeneralInfoBox;
        private Button BindProjectButton;
        private TabControl ActionTabs;
        private TabPage ModuleViewsTabPage;
        private Label BoundProjectLabel;
        private Button FixupCopyrightButton;
        private Panel ModuleViewPanel;
        private TableLayoutPanel ModuleViewsLayout;
        private Button NewModuleButton;
        private Panel ModuleToolboxPanel;
        private Button DeleteEmptyDirsButton;
        private Panel ProjectActionsPanel;
        private TableLayoutPanel FormLayoutPanel;
        private Label ToolName;
        private Panel ProjectInfoPanel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainFormLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.GeneralInfoBox = new System.Windows.Forms.GroupBox();
            this.ProjectActionsPanel = new System.Windows.Forms.Panel();
            this.FixupCopyrightButton = new System.Windows.Forms.Button();
            this.DeleteEmptyDirsButton = new System.Windows.Forms.Button();
            this.ProjectInfoPanel = new System.Windows.Forms.Panel();
            this.BindProjectButton = new System.Windows.Forms.Button();
            this.ActionsPanel = new System.Windows.Forms.Panel();
            this.ActionTabs = new System.Windows.Forms.TabControl();
            this.ModuleViewsTabPage = new System.Windows.Forms.TabPage();
            this.ModuleViewsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ModuleToolboxPanel = new System.Windows.Forms.Panel();
            this.NewModuleButton = new System.Windows.Forms.Button();
            this.ModuleViewPanel = new System.Windows.Forms.Panel();
            this.BoundProjectLabel = new System.Windows.Forms.Label();
            this.FormLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ToolName = new System.Windows.Forms.Label();
            this.MainFormLayoutPanel.SuspendLayout();
            this.InfoPanel.SuspendLayout();
            this.GeneralInfoBox.SuspendLayout();
            this.ProjectActionsPanel.SuspendLayout();
            this.ActionsPanel.SuspendLayout();
            this.ActionTabs.SuspendLayout();
            this.ModuleViewsTabPage.SuspendLayout();
            this.ModuleViewsLayout.SuspendLayout();
            this.ModuleToolboxPanel.SuspendLayout();
            this.FormLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormLayoutPanel
            // 
            this.MainFormLayoutPanel.AutoSize = true;
            this.MainFormLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainFormLayoutPanel.ColumnCount = 2;
            this.MainFormLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.MainFormLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.MainFormLayoutPanel.Controls.Add(this.InfoPanel, 0, 0);
            this.MainFormLayoutPanel.Controls.Add(this.ActionsPanel, 1, 0);
            this.MainFormLayoutPanel.Controls.Add(this.BoundProjectLabel, 0, 1);
            this.MainFormLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainFormLayoutPanel.Location = new System.Drawing.Point(3, 78);
            this.MainFormLayoutPanel.Name = "MainFormLayoutPanel";
            this.MainFormLayoutPanel.RowCount = 2;
            this.MainFormLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainFormLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainFormLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainFormLayoutPanel.Size = new System.Drawing.Size(1178, 480);
            this.MainFormLayoutPanel.TabIndex = 0;
            // 
            // InfoPanel
            // 
            this.InfoPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.InfoPanel.Controls.Add(this.GeneralInfoBox);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoPanel.Location = new System.Drawing.Point(3, 3);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Padding = new System.Windows.Forms.Padding(10);
            this.InfoPanel.Size = new System.Drawing.Size(347, 454);
            this.InfoPanel.TabIndex = 0;
            // 
            // GeneralInfoBox
            // 
            this.GeneralInfoBox.BackColor = System.Drawing.Color.Transparent;
            this.GeneralInfoBox.Controls.Add(this.ProjectActionsPanel);
            this.GeneralInfoBox.Controls.Add(this.ProjectInfoPanel);
            this.GeneralInfoBox.Controls.Add(this.BindProjectButton);
            this.GeneralInfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralInfoBox.Location = new System.Drawing.Point(10, 10);
            this.GeneralInfoBox.Name = "GeneralInfoBox";
            this.GeneralInfoBox.Size = new System.Drawing.Size(327, 434);
            this.GeneralInfoBox.TabIndex = 0;
            this.GeneralInfoBox.TabStop = false;
            this.GeneralInfoBox.Text = "General Info";
            // 
            // ProjectActionsPanel
            // 
            this.ProjectActionsPanel.AutoSize = true;
            this.ProjectActionsPanel.Controls.Add(this.FixupCopyrightButton);
            this.ProjectActionsPanel.Controls.Add(this.DeleteEmptyDirsButton);
            this.ProjectActionsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProjectActionsPanel.Location = new System.Drawing.Point(3, 385);
            this.ProjectActionsPanel.Name = "ProjectActionsPanel";
            this.ProjectActionsPanel.Size = new System.Drawing.Size(321, 46);
            this.ProjectActionsPanel.TabIndex = 2;
            // 
            // FixupCopyrightButton
            // 
            this.FixupCopyrightButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FixupCopyrightButton.Enabled = false;
            this.FixupCopyrightButton.Location = new System.Drawing.Point(0, 0);
            this.FixupCopyrightButton.Margin = new System.Windows.Forms.Padding(20);
            this.FixupCopyrightButton.Name = "FixupCopyrightButton";
            this.FixupCopyrightButton.Size = new System.Drawing.Size(321, 23);
            this.FixupCopyrightButton.TabIndex = 0;
            this.FixupCopyrightButton.Text = "Could not find Source";
            this.FixupCopyrightButton.UseVisualStyleBackColor = true;
            this.FixupCopyrightButton.Click += new System.EventHandler(this.FixupCopywriteButton_Click);
            // 
            // DeleteEmptyDirsButton
            // 
            this.DeleteEmptyDirsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DeleteEmptyDirsButton.Enabled = false;
            this.DeleteEmptyDirsButton.Location = new System.Drawing.Point(0, 23);
            this.DeleteEmptyDirsButton.Name = "DeleteEmptyDirsButton";
            this.DeleteEmptyDirsButton.Size = new System.Drawing.Size(321, 23);
            this.DeleteEmptyDirsButton.TabIndex = 1;
            this.DeleteEmptyDirsButton.Text = "Could not find Source";
            this.DeleteEmptyDirsButton.UseVisualStyleBackColor = true;
            this.DeleteEmptyDirsButton.Click += new System.EventHandler(this.DeleteEmptyDirsButton_Click);
            // 
            // ProjectInfoPanel
            // 
            this.ProjectInfoPanel.AutoSize = true;
            this.ProjectInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProjectInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectInfoPanel.Location = new System.Drawing.Point(3, 42);
            this.ProjectInfoPanel.Name = "ProjectInfoPanel";
            this.ProjectInfoPanel.Size = new System.Drawing.Size(321, 389);
            this.ProjectInfoPanel.TabIndex = 1;
            this.ProjectInfoPanel.Visible = false;
            // 
            // BindProjectButton
            // 
            this.BindProjectButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.BindProjectButton.Location = new System.Drawing.Point(3, 17);
            this.BindProjectButton.Name = "BindProjectButton";
            this.BindProjectButton.Size = new System.Drawing.Size(321, 25);
            this.BindProjectButton.TabIndex = 0;
            this.BindProjectButton.Text = "Bind Project";
            this.BindProjectButton.UseVisualStyleBackColor = true;
            this.BindProjectButton.Click += new System.EventHandler(this.BindProjectButton_Click);
            // 
            // ActionsPanel
            // 
            this.ActionsPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.ActionsPanel.Controls.Add(this.ActionTabs);
            this.ActionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActionsPanel.Location = new System.Drawing.Point(356, 3);
            this.ActionsPanel.Name = "ActionsPanel";
            this.ActionsPanel.Size = new System.Drawing.Size(819, 454);
            this.ActionsPanel.TabIndex = 1;
            // 
            // ActionTabs
            // 
            this.ActionTabs.Controls.Add(this.ModuleViewsTabPage);
            this.ActionTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActionTabs.Location = new System.Drawing.Point(0, 0);
            this.ActionTabs.Name = "ActionTabs";
            this.ActionTabs.SelectedIndex = 1;
            this.ActionTabs.Size = new System.Drawing.Size(819, 454);
            this.ActionTabs.TabIndex = 0;
            // 
            // ModuleViewsTabPage
            // 
            this.ModuleViewsTabPage.Controls.Add(this.ModuleViewsLayout);
            this.ModuleViewsTabPage.Location = new System.Drawing.Point(4, 25);
            this.ModuleViewsTabPage.Name = "ModuleViewsTabPage";
            this.ModuleViewsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ModuleViewsTabPage.Size = new System.Drawing.Size(811, 425);
            this.ModuleViewsTabPage.TabIndex = 0;
            this.ModuleViewsTabPage.Text = "Modules";
            this.ModuleViewsTabPage.UseVisualStyleBackColor = true;
            // 
            // ModuleViewsLayout
            // 
            this.ModuleViewsLayout.AutoSize = true;
            this.ModuleViewsLayout.BackColor = System.Drawing.Color.Gainsboro;
            this.ModuleViewsLayout.ColumnCount = 1;
            this.ModuleViewsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ModuleViewsLayout.Controls.Add(this.ModuleToolboxPanel, 0, 0);
            this.ModuleViewsLayout.Controls.Add(this.ModuleViewPanel, 0, 1);
            this.ModuleViewsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModuleViewsLayout.Enabled = false;
            this.ModuleViewsLayout.Location = new System.Drawing.Point(3, 3);
            this.ModuleViewsLayout.Name = "ModuleViewsLayout";
            this.ModuleViewsLayout.RowCount = 3;
            this.ModuleViewsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.ModuleViewsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ModuleViewsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ModuleViewsLayout.Size = new System.Drawing.Size(805, 419);
            this.ModuleViewsLayout.TabIndex = 1;
            // 
            // ModuleToolboxPanel
            // 
            this.ModuleToolboxPanel.AutoSize = true;
            this.ModuleToolboxPanel.Controls.Add(this.NewModuleButton);
            this.ModuleToolboxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModuleToolboxPanel.Location = new System.Drawing.Point(3, 3);
            this.ModuleToolboxPanel.Name = "ModuleToolboxPanel";
            this.ModuleToolboxPanel.Padding = new System.Windows.Forms.Padding(5);
            this.ModuleToolboxPanel.Size = new System.Drawing.Size(799, 94);
            this.ModuleToolboxPanel.TabIndex = 0;
            // 
            // NewModuleButton
            // 
            this.NewModuleButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.NewModuleButton.Location = new System.Drawing.Point(5, 66);
            this.NewModuleButton.Name = "NewModuleButton";
            this.NewModuleButton.Size = new System.Drawing.Size(789, 23);
            this.NewModuleButton.TabIndex = 1;
            this.NewModuleButton.Text = "Add New Module";
            this.NewModuleButton.UseVisualStyleBackColor = true;
            this.NewModuleButton.Click += new System.EventHandler(this.NewModuleButton_Click);
            // 
            // ModuleViewPanel
            // 
            this.ModuleViewPanel.AutoScroll = true;
            this.ModuleViewPanel.AutoSize = true;
            this.ModuleViewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModuleViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModuleViewPanel.Location = new System.Drawing.Point(3, 103);
            this.ModuleViewPanel.Name = "ModuleViewPanel";
            this.ModuleViewPanel.Size = new System.Drawing.Size(799, 293);
            this.ModuleViewPanel.TabIndex = 0;
            // 
            // BoundProjectLabel
            // 
            this.BoundProjectLabel.AutoSize = true;
            this.BoundProjectLabel.Location = new System.Drawing.Point(3, 460);
            this.BoundProjectLabel.Name = "BoundProjectLabel";
            this.BoundProjectLabel.Size = new System.Drawing.Size(296, 16);
            this.BoundProjectLabel.TabIndex = 3;
            this.BoundProjectLabel.Text = "Press [Bind Project] to point UPT to a valid uproject file";
            this.BoundProjectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormLayoutPanel
            // 
            this.FormLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.FormLayoutPanel.ColumnCount = 1;
            this.FormLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.FormLayoutPanel.Controls.Add(this.MainFormLayoutPanel, 0, 1);
            this.FormLayoutPanel.Controls.Add(this.ToolName, 0, 0);
            this.FormLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.FormLayoutPanel.Name = "FormLayoutPanel";
            this.FormLayoutPanel.RowCount = 2;
            this.FormLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.FormLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.FormLayoutPanel.Size = new System.Drawing.Size(1184, 561);
            this.FormLayoutPanel.TabIndex = 0;
            // 
            // ToolName
            // 
            this.ToolName.AutoSize = true;
            this.ToolName.BackColor = System.Drawing.Color.Transparent;
            this.ToolName.Dock = System.Windows.Forms.DockStyle.Left;
            this.ToolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ToolName.Location = new System.Drawing.Point(3, 0);
            this.ToolName.Name = "ToolName";
            this.ToolName.Size = new System.Drawing.Size(328, 75);
            this.ToolName.TabIndex = 1;
            this.ToolName.Text = "Unreal Project Tool";
            this.ToolName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.FormLayoutPanel);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MainFormLayoutPanel.ResumeLayout(false);
            this.MainFormLayoutPanel.PerformLayout();
            this.InfoPanel.ResumeLayout(false);
            this.GeneralInfoBox.ResumeLayout(false);
            this.GeneralInfoBox.PerformLayout();
            this.ProjectActionsPanel.ResumeLayout(false);
            this.ActionsPanel.ResumeLayout(false);
            this.ActionTabs.ResumeLayout(false);
            this.ModuleViewsTabPage.ResumeLayout(false);
            this.ModuleViewsTabPage.PerformLayout();
            this.ModuleViewsLayout.ResumeLayout(false);
            this.ModuleViewsLayout.PerformLayout();
            this.ModuleToolboxPanel.ResumeLayout(false);
            this.FormLayoutPanel.ResumeLayout(false);
            this.FormLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        //Find a Uproject file to open
        private void BindProjectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog FindProjectDialog = new OpenFileDialog();

            FindProjectDialog.Title = "Browse for a uProject file to bind to";
            FindProjectDialog.InitialDirectory = @"C:\";
            FindProjectDialog.DefaultExt = ".uproject";
            FindProjectDialog.Filter = "Unreal Project Files (*.uproject)|*uproject";

            FindProjectDialog.ShowDialog();

            string BoundProjectDir = FindProjectDialog.FileName;

            if (BoundProjectDir != "")
            {
                //Critical - caches all paths and files all necessary files
                ProjectWorker = new UProjectWorker(BoundProjectDir);

                BoundProjectLabel.Text = "Project found at: " + BoundProjectDir;

                BuildProjectInfoPanel();
                BuildModulePanel();
            }
        }

        private void BuildProjectInfoPanel()
        {
            ProjectInfoPanel.Controls.Clear();
            ProjectInfoPanel.Visible = true;
            
            List<Label> NewLabels = new List<Label>();

            Label ProjectNameLabel = new Label();
            Label CopywriteLabel = new Label();
            Label ProjectVersionLabel = new Label();    
            NewLabels.Insert(0, ProjectNameLabel);
            NewLabels.Insert(0, CopywriteLabel);
            NewLabels.Insert(0, ProjectVersionLabel);

            ProjectNameLabel.Text = "Project Name: " + ProjectWorker.DefaultGameConfigReader.GetValForKey(@"ProjectName");
            CopywriteLabel.Text = "Copywrite Notice: " + ProjectWorker.DefaultGameConfigReader.GetValForKey(@"CopyrightNotice");
            ProjectVersionLabel.Text = "Project Version: " + ProjectWorker.DefaultGameConfigReader.GetValForKey(@"ProjectVersion");

            foreach (Label NewLabel in NewLabels)
            {
                ProjectInfoPanel.Controls.Add(NewLabel);
                NewLabel.Dock = DockStyle.Top;
                NewLabel.AutoSize = true;
            }

            if (ProjectWorker.SourceDirectory != "")
            {
                FixupCopyrightButton.Enabled = true;
                FixupCopyrightButton.Text = "Fixup copyright";

                DeleteEmptyDirsButton.Enabled = true;
                DeleteEmptyDirsButton.Text = "Delete empty directories";
            }
        }

        private void BuildModulePanel()
        {
            ModuleViewPanel.Controls.Clear();
            ModuleViewsLayout.Enabled = true;

            foreach (ModuleData Data in ProjectWorker.Proxy.Modules)
            {
                ModuleView NewModuleView = new ModuleView(Data);
                NewModuleView.Dock = DockStyle.Top;

                ModuleViewPanel.Controls.Add(NewModuleView);
                ModuleViewPanel.Update();
            }
        }

        private void FixupCopywriteButton_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show(@"This operation may take a while, would you like to proceed?", @"Fixup Copyright", MessageBoxButtons.YesNo);

            if(Result == DialogResult.Yes)
            {
                StartSourceScan();
            }
        }   
        
        private void StartSourceScan()
        {
            SourceScanOutput = new Form();
            SourceScanOutput.Controls.Clear();

            SourceScanOutput.Text = @"Scanning source for noncompliant files";
            SourceScanOutput.FormBorderStyle = FormBorderStyle.FixedSingle;
            SourceScanOutput.StartPosition = FormStartPosition.WindowsDefaultLocation;
            SourceScanOutput.Size = new System.Drawing.Size(1000, 200);
            SourceScanOutput.BackColor = Color.Black;

            Panel NewPanel = new Panel();
            NewPanel.BorderStyle = BorderStyle.FixedSingle;
            NewPanel.Dock = DockStyle.Fill;
            NewPanel.AutoScroll = true;
            NewPanel.SetAutoScrollMargin(10, 10);

            SourceScanOutput.Controls.Add(NewPanel);
            SourceScanOutput.Show();

            string[] Files = Directory.GetFiles(ProjectWorker.SourceDirectory, "*.*", SearchOption.AllDirectories);
            Dictionary<string, string> InvalidFiles = new Dictionary<string, string>();
            int FileNum = 1;

            foreach (string File in Files)
            {
                Label CurrentFileLabel = new Label();   
                CurrentFileLabel.ForeColor = Color.White;

                CurrentFileLabel.Dock = DockStyle.Top;
                CurrentFileLabel.Text = "Checking " + Path.GetFileName(File) + "(" + FileNum + "/" + Files.Length + ")";

                NewPanel.Controls.Add(CurrentFileLabel);

                Label FileStatusLabel = new Label();
                FileStatusLabel.Dock = DockStyle.Top;

                string IncorrectFirstLine = "";
                if (CheckFileHasCorrectCopyright(File, out IncorrectFirstLine))
                {
                    FileStatusLabel.ForeColor = Color.LawnGreen;
                    FileStatusLabel.Text = "File compliant";
                }
                else
                {
                    FileStatusLabel.ForeColor = Color.Red;
                    FileStatusLabel.Text = "File noncompliant - first line reads " + IncorrectFirstLine;
                    InvalidFiles.Add(File, IncorrectFirstLine);
                }

                NewPanel.Controls.Add(FileStatusLabel);
                NewPanel.Update();

                FileNum++;
            }

            if(InvalidFiles.Count > 0)
            {
                string InvalidFileList = System.Environment.NewLine;

                foreach (KeyValuePair<string, string> InvalidFile in InvalidFiles)
                {
                    InvalidFileList += InvalidFile.Key + System.Environment.NewLine;
                }

                DialogResult Result = MessageBox.Show(@"Would you like to correct the copyright notice on " + InvalidFiles.Count + " files?", @"Fixup Files",  MessageBoxButtons.YesNo);

                if(Result == DialogResult.Yes || Result == DialogResult.No)
                {
                    SourceScanOutput.Close();
                }

                if (Result == DialogResult.Yes)
                {
                    TryApplyCopyrightToFiles(InvalidFiles);
                }
            }
        }

        private bool CheckFileHasCorrectCopyright(string InFile, out string IncorrectFirstLine) 
        {
            string FirstLine = File.ReadLines(InFile).First();
            string CopyrightNotice = ProjectWorker.DefaultGameConfigReader.GetValForKey(@"CopyrightNotice");

            if (FirstLine != ("// " + CopyrightNotice))
            {
                IncorrectFirstLine = FirstLine;
                return false;
            }

            IncorrectFirstLine = "";
            return true;
        }

        private void TryApplyCopyrightToFiles(Dictionary<string, string> InFiles)
        {
            string NewCopyrightLine = "// " + ProjectWorker.DefaultGameConfigReader.GetValForKey(@"CopyrightNotice");

            int NumFixes = 0;
            foreach(KeyValuePair<string, string> FilePair in InFiles)
            {
                string CurrentFirstLine = FilePair.Value;
                string[] FileCopy = File.ReadAllLines(FilePair.Key);

                if (CurrentFirstLine.Contains(@"//"))
                {
                    Debug.WriteLine("First line is a comment");

                    FileCopy[0] = NewCopyrightLine;

                    if(FileCopy[1] != "")
                    {
                        List<string> CopyAsList = new List<string>();
                        CopyAsList = FileCopy.ToList<string>();
                        CopyAsList.Insert(1, ""); //Create a new line

                        FileCopy = CopyAsList.ToArray();
                    }

                    WriteToFile(FilePair.Key, FileCopy);
                    NumFixes++;
                }
                else if (CurrentFirstLine.Contains(@"#") || CurrentFirstLine.Contains(@"using"))
                {
                    Debug.WriteLine("First line is an include or a pragma, or is importing an external library for a build or target file");

                    List<string> CopyAsList = new List<string>();
                    CopyAsList = FileCopy.ToList<string>();

                    CopyAsList.Insert(0, "");
                    CopyAsList.Insert(0, NewCopyrightLine);

                    FileCopy = CopyAsList.ToArray();

                    WriteToFile(FilePair.Key, FileCopy);
                    NumFixes++;
                }
                else
                {
                    Debug.WriteLine("File " + FilePair.Key + "has an unknown usecase - not altering anything for the time being");
                }
            }

            MessageBox.Show("Fixed up " + NumFixes + " files", @"Copyright fixup results");
        }

        private void WriteToFile(string InPath, string[] NewFileText)
        {
            System.IO.File.WriteAllLines(InPath, NewFileText);
        }

        private void NewModuleButton_Click(object sender, EventArgs e)
        {
            NewModuleForm ModuleForm = new NewModuleForm();

            ModuleForm.CurrentProjectProxy = ProjectWorker.Proxy;

            ModuleForm.Show();

            ModuleForm.OnNewModuleCreated = OnNewModuleCreated;
        }

        private void OnNewModuleCreated(ModuleData InNewModuleData)
        {
            ModuleView NewModuleView = new ModuleView(InNewModuleData);
            NewModuleView.Dock = DockStyle.Top;

            ModuleViewPanel.Controls.Add(NewModuleView);
            ProjectWorker.AddModuleToProxy(InNewModuleData);

            ZipFile.ExtractToDirectory(ProjectWorker.EmptyModuleFiles, Path.Combine(ProjectWorker.SourceDirectory, "Runtime"));

            string NewDirectoryName = Path.Combine(ProjectWorker.SourceDirectory, InNewModuleData.Type, InNewModuleData.Name);


            Directory.Move(Path.Combine(ProjectWorker.SourceDirectory, "Runtime", EmptyModuleToken), NewDirectoryName);

            string[] Files = Directory.GetFiles(NewDirectoryName);

            foreach (string CurrFile in Files)
            {
                string text = File.ReadAllText(CurrFile);
                text = text.Replace(EmptyModuleToken, InNewModuleData.Name);
                File.WriteAllText(CurrFile, text);

                System.IO.File.Move(CurrFile, CurrFile.Replace(EmptyModuleToken, InNewModuleData.Name));
            }

            if (InNewModuleData.Type == "Runtime")
            {
                string[] PrimaryBuildFileContents = File.ReadAllLines(ProjectWorker.PrimaryGameplayBuildFile);
                List<string> ContentsAsList = new List<string>();
                ContentsAsList = PrimaryBuildFileContents.ToList<string>();

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
                        return Line.Contains(@"public " + ProjectWorker.PrimaryModuleName);
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

                File.WriteAllLines(ProjectWorker.PrimaryGameplayBuildFile, ContentsAsList.ToArray());
            }

            SaveProject();
        }

        private int GetDependencyInjectionLine(List<string> InContent)
        {
            List<string> ContentCopy = InContent;

            for (int i = 0; i < ContentCopy.Count; ++i)
            {
                ContentCopy[i] = String.Concat(ContentCopy[i].Where(c => !Char.IsWhiteSpace(c)));
            }

            if (ContentCopy.Contains(UProjectWorker.DependencyInjectionString))
            {
                int InjectionIndex = InContent.FindIndex(
                delegate (string Line)
                {
                    return Line.Contains(UProjectWorker.DependencyInjectionString);
                });

                return InjectionIndex + 1;
            }

            return -1;
        }

        private void SaveProject()
        {
            ProjectWorker.Save();
        }

        private void DeleteEmptyDirsButton_Click(object sender, EventArgs e)
        {
            CheckDirectoryIsEmpty(ProjectWorker.SourceDirectory);
        }

        private void CheckDirectoryIsEmpty(string InDirectory)
        {
            foreach (string Dir in Directory.GetDirectories(InDirectory))
            {
                CheckDirectoryIsEmpty(Dir);
                if (Directory.GetFiles(Dir).Length == 0 &&
                    Directory.GetDirectories(Dir).Length == 0)
                {
                    Directory.Delete(Dir, false);
                }
            }
        }

        private Form SourceScanOutput = new Form();
        private UProjectWorker ProjectWorker;
        static string EmptyModuleToken = @"Empty";
    }
}
