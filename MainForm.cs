using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Linq;

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
        private TableLayoutPanel tableLayoutPanel1;
        private Button NewModuleButton;
        private Panel ModuleToolboxPanel;
        private Panel ProjectInfoPanel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.MainFormLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.GeneralInfoBox = new System.Windows.Forms.GroupBox();
            this.ProjectInfoPanel = new System.Windows.Forms.Panel();
            this.FixupCopyrightButton = new System.Windows.Forms.Button();
            this.BindProjectButton = new System.Windows.Forms.Button();
            this.ActionsPanel = new System.Windows.Forms.Panel();
            this.ActionTabs = new System.Windows.Forms.TabControl();
            this.ModuleViewsTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ModuleViewPanel = new System.Windows.Forms.Panel();
            this.NewModuleButton = new System.Windows.Forms.Button();
            this.BoundProjectLabel = new System.Windows.Forms.Label();
            this.ModuleToolboxPanel = new System.Windows.Forms.Panel();
            this.MainFormLayoutPanel.SuspendLayout();
            this.InfoPanel.SuspendLayout();
            this.GeneralInfoBox.SuspendLayout();
            this.ProjectInfoPanel.SuspendLayout();
            this.ActionsPanel.SuspendLayout();
            this.ActionTabs.SuspendLayout();
            this.ModuleViewsTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.ModuleToolboxPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormLayoutPanel
            // 
            this.MainFormLayoutPanel.ColumnCount = 2;
            this.MainFormLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.MainFormLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.MainFormLayoutPanel.Controls.Add(this.InfoPanel, 0, 0);
            this.MainFormLayoutPanel.Controls.Add(this.ActionsPanel, 1, 0);
            this.MainFormLayoutPanel.Controls.Add(this.BoundProjectLabel, 0, 1);
            this.MainFormLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainFormLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainFormLayoutPanel.Name = "MainFormLayoutPanel";
            this.MainFormLayoutPanel.RowCount = 2;
            this.MainFormLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainFormLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainFormLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainFormLayoutPanel.Size = new System.Drawing.Size(1201, 531);
            this.MainFormLayoutPanel.TabIndex = 0;
            // 
            // InfoPanel
            // 
            this.InfoPanel.BackColor = System.Drawing.SystemColors.Control;
            this.InfoPanel.Controls.Add(this.GeneralInfoBox);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoPanel.Location = new System.Drawing.Point(3, 3);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Padding = new System.Windows.Forms.Padding(10);
            this.InfoPanel.Size = new System.Drawing.Size(354, 505);
            this.InfoPanel.TabIndex = 0;
            // 
            // GeneralInfoBox
            // 
            this.GeneralInfoBox.BackColor = System.Drawing.SystemColors.Control;
            this.GeneralInfoBox.Controls.Add(this.ProjectInfoPanel);
            this.GeneralInfoBox.Controls.Add(this.BindProjectButton);
            this.GeneralInfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralInfoBox.Location = new System.Drawing.Point(10, 10);
            this.GeneralInfoBox.Name = "GeneralInfoBox";
            this.GeneralInfoBox.Size = new System.Drawing.Size(334, 485);
            this.GeneralInfoBox.TabIndex = 0;
            this.GeneralInfoBox.TabStop = false;
            this.GeneralInfoBox.Text = "General Info";
            // 
            // ProjectInfoPanel
            // 
            this.ProjectInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProjectInfoPanel.Controls.Add(this.FixupCopyrightButton);
            this.ProjectInfoPanel.Location = new System.Drawing.Point(6, 50);
            this.ProjectInfoPanel.Name = "ProjectInfoPanel";
            this.ProjectInfoPanel.Size = new System.Drawing.Size(322, 429);
            this.ProjectInfoPanel.TabIndex = 1;
            this.ProjectInfoPanel.Visible = false;
            // 
            // FixupCopyrightButton
            // 
            this.FixupCopyrightButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FixupCopyrightButton.Enabled = false;
            this.FixupCopyrightButton.Location = new System.Drawing.Point(0, 404);
            this.FixupCopyrightButton.Margin = new System.Windows.Forms.Padding(20);
            this.FixupCopyrightButton.Name = "FixupCopyrightButton";
            this.FixupCopyrightButton.Size = new System.Drawing.Size(320, 23);
            this.FixupCopyrightButton.TabIndex = 0;
            this.FixupCopyrightButton.Text = "Could not find Source";
            this.FixupCopyrightButton.UseVisualStyleBackColor = true;
            this.FixupCopyrightButton.Click += new System.EventHandler(this.FixupCopywriteButton_Click);
            // 
            // BindProjectButton
            // 
            this.BindProjectButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.BindProjectButton.Location = new System.Drawing.Point(3, 16);
            this.BindProjectButton.Name = "BindProjectButton";
            this.BindProjectButton.Size = new System.Drawing.Size(328, 25);
            this.BindProjectButton.TabIndex = 0;
            this.BindProjectButton.Text = "Bind Project";
            this.BindProjectButton.UseVisualStyleBackColor = true;
            this.BindProjectButton.Click += new System.EventHandler(this.BindProjectButton_Click);
            // 
            // ActionsPanel
            // 
            this.ActionsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ActionsPanel.Controls.Add(this.ActionTabs);
            this.ActionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActionsPanel.Location = new System.Drawing.Point(363, 3);
            this.ActionsPanel.Name = "ActionsPanel";
            this.ActionsPanel.Size = new System.Drawing.Size(835, 505);
            this.ActionsPanel.TabIndex = 1;
            // 
            // ActionTabs
            // 
            this.ActionTabs.Controls.Add(this.ModuleViewsTabPage);
            this.ActionTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActionTabs.Location = new System.Drawing.Point(0, 0);
            this.ActionTabs.Name = "ActionTabs";
            this.ActionTabs.SelectedIndex = 1;
            this.ActionTabs.Size = new System.Drawing.Size(835, 505);
            this.ActionTabs.TabIndex = 0;
            // 
            // ModuleViewsTabPage
            // 
            this.ModuleViewsTabPage.Controls.Add(this.tableLayoutPanel1);
            this.ModuleViewsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ModuleViewsTabPage.Name = "ModuleViewsTabPage";
            this.ModuleViewsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ModuleViewsTabPage.Size = new System.Drawing.Size(827, 479);
            this.ModuleViewsTabPage.TabIndex = 0;
            this.ModuleViewsTabPage.Text = "Modules";
            this.ModuleViewsTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ModuleToolboxPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ModuleViewPanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Enabled = false;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(821, 473);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ModuleViewPanel
            // 
            this.ModuleViewPanel.AutoScroll = true;
            this.ModuleViewPanel.AutoSize = true;
            this.ModuleViewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModuleViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModuleViewPanel.Location = new System.Drawing.Point(3, 103);
            this.ModuleViewPanel.Name = "ModuleViewPanel";
            this.ModuleViewPanel.Size = new System.Drawing.Size(815, 347);
            this.ModuleViewPanel.TabIndex = 0;
            // 
            // NewModuleButton
            // 
            this.NewModuleButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.NewModuleButton.Location = new System.Drawing.Point(5, 66);
            this.NewModuleButton.Name = "NewModuleButton";
            this.NewModuleButton.Size = new System.Drawing.Size(805, 23);
            this.NewModuleButton.TabIndex = 1;
            this.NewModuleButton.Text = "Add New Module";
            this.NewModuleButton.UseVisualStyleBackColor = true;
            // 
            // BoundProjectLabel
            // 
            this.BoundProjectLabel.AutoSize = true;
            this.BoundProjectLabel.Location = new System.Drawing.Point(3, 511);
            this.BoundProjectLabel.Name = "BoundProjectLabel";
            this.BoundProjectLabel.Size = new System.Drawing.Size(265, 13);
            this.BoundProjectLabel.TabIndex = 3;
            this.BoundProjectLabel.Text = "Press [Bind Project] to point UPT to a valid uproject file";
            this.BoundProjectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ModuleToolboxPanel
            // 
            this.ModuleToolboxPanel.Controls.Add(this.NewModuleButton);
            this.ModuleToolboxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModuleToolboxPanel.Location = new System.Drawing.Point(3, 3);
            this.ModuleToolboxPanel.Name = "ModuleToolboxPanel";
            this.ModuleToolboxPanel.Padding = new System.Windows.Forms.Padding(5);
            this.ModuleToolboxPanel.Size = new System.Drawing.Size(815, 94);
            this.ModuleToolboxPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1201, 531);
            this.Controls.Add(this.MainFormLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unreal Project Tool";
            this.MainFormLayoutPanel.ResumeLayout(false);
            this.MainFormLayoutPanel.PerformLayout();
            this.InfoPanel.ResumeLayout(false);
            this.GeneralInfoBox.ResumeLayout(false);
            this.ProjectInfoPanel.ResumeLayout(false);
            this.ActionsPanel.ResumeLayout(false);
            this.ActionTabs.ResumeLayout(false);
            this.ModuleViewsTabPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ModuleToolboxPanel.ResumeLayout(false);
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

            BoundProjectDir = FindProjectDialog.FileName;

            if (BoundProjectDir != "")
            {
                BoundProjectLabel.Text = "Project found at: " + BoundProjectDir;

                if (CacheDefaultGamePath())
                {
                    BuildProjectInfoPanel();
                }

                CacheSourcePath();
            }
        }

        private bool CacheDefaultGamePath()
        {
            if (BoundProjectDir != "")
            {
                string[] Dirs = Directory.GetDirectories(Path.GetDirectoryName(BoundProjectDir));

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
            }

            return DefaultGameConfig != "";
        }

        private void BuildProjectInfoPanel()
        {
            ProjectInfoPanel.Visible = true;

            DefaultGameConfigReader = new IniReader(DefaultGameConfig);

            List<Label> NewLabels = new List<Label>();

            Label ProjectNameLabel = new Label();
            Label CopywriteLabel = new Label();
            Label ProjectVersionLabel = new Label();
            NewLabels.Insert(0, ProjectNameLabel);
            NewLabels.Insert(0, CopywriteLabel);
            NewLabels.Insert(0, ProjectVersionLabel);

            ProjectNameLabel.Text = "Project Name: " + DefaultGameConfigReader.GetValForKey(@"ProjectName");
            CopywriteLabel.Text = "Copywrite Notice: " + DefaultGameConfigReader.GetValForKey(@"CopyrightNotice");
            ProjectVersionLabel.Text = "Project Version: " + DefaultGameConfigReader.GetValForKey(@"ProjectVersion");

            foreach (Label NewLabel in NewLabels)
            {
                ProjectInfoPanel.Controls.Add(NewLabel);
                NewLabel.Dock = DockStyle.Top;
                NewLabel.AutoSize = true;
            }
        }

        private void CacheSourcePath()
        {
            string[] Dirs = Directory.GetDirectories(Path.GetDirectoryName(BoundProjectDir));

            foreach (string Dir in Dirs)
            {
                if (Dir.Contains(@"Source"))
                {
                    SourceDirectory = Dir;
                    break;
                }
            }

            if (SourceDirectory != "")
            {
                FixupCopyrightButton.Enabled = true;
                FixupCopyrightButton.Text = "Fixup copyright";
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

            string[] Files = Directory.GetFiles(SourceDirectory, "*.*", SearchOption.AllDirectories);
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

                DialogResult Result = MessageBox.Show(
                    @"Would you like to correct the copyright notice on the following files?" + System.Environment.NewLine + InvalidFileList,
                    @"Fixup Files",
                    MessageBoxButtons.YesNo);

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
            string CopyrightNotice = DefaultGameConfigReader.GetValForKey(@"CopyrightNotice");

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
            string NewCopyrightLine = "// " + DefaultGameConfigReader.GetValForKey(@"CopyrightNotice");

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

        public string BoundProjectDir = "";
        public string DefaultGameConfig = "";
        public string SourceDirectory = "";
        private Form SourceScanOutput = new Form();
        private IniReader DefaultGameConfigReader;
    }
}
