namespace UnrealProjectTool
{
    partial class NewModuleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ModuleNameBox = new System.Windows.Forms.TextBox();
            this.CreateModuleButton = new System.Windows.Forms.Button();
            this.LoadingPhaseBox = new System.Windows.Forms.ComboBox();
            this.TypeBox = new System.Windows.Forms.ComboBox();
            this.ErrorPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ModuleNameBox
            // 
            this.ModuleNameBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ModuleNameBox.Location = new System.Drawing.Point(20, 20);
            this.ModuleNameBox.Name = "ModuleNameBox";
            this.ModuleNameBox.Size = new System.Drawing.Size(337, 20);
            this.ModuleNameBox.TabIndex = 0;
            this.ModuleNameBox.TextChanged += new System.EventHandler(this.ModuleNameBox_TextChanged);
            // 
            // CreateModuleButton
            // 
            this.CreateModuleButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CreateModuleButton.Enabled = false;
            this.CreateModuleButton.Location = new System.Drawing.Point(20, 144);
            this.CreateModuleButton.Name = "CreateModuleButton";
            this.CreateModuleButton.Size = new System.Drawing.Size(337, 23);
            this.CreateModuleButton.TabIndex = 1;
            this.CreateModuleButton.Text = "Cannot Add Module";
            this.CreateModuleButton.UseVisualStyleBackColor = true;
            this.CreateModuleButton.Click += new System.EventHandler(this.CreateModuleButton_Click);
            // 
            // LoadingPhaseBox
            // 
            this.LoadingPhaseBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.LoadingPhaseBox.FormattingEnabled = true;
            this.LoadingPhaseBox.Items.AddRange(new object[] {
            "EarliestPossible",
            "PostConfigInit",
            "PostSplashScreen",
            "PreEarlyLoadingScreen",
            "PreLoadingScreen",
            "PreDefault",
            "Default",
            "PostDefault",
            "PostEngineInit",
            "None"});
            this.LoadingPhaseBox.Location = new System.Drawing.Point(20, 40);
            this.LoadingPhaseBox.Name = "LoadingPhaseBox";
            this.LoadingPhaseBox.Size = new System.Drawing.Size(337, 21);
            this.LoadingPhaseBox.TabIndex = 2;
            this.LoadingPhaseBox.Text = "Default";
            // 
            // TypeBox
            // 
            this.TypeBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.TypeBox.FormattingEnabled = true;
            this.TypeBox.Items.AddRange(new object[] {
            "Runtime",
            "Editor"});
            this.TypeBox.Location = new System.Drawing.Point(20, 61);
            this.TypeBox.Name = "TypeBox";
            this.TypeBox.Size = new System.Drawing.Size(337, 21);
            this.TypeBox.TabIndex = 3;
            this.TypeBox.Text = "Runtime";
            // 
            // ErrorPanel
            // 
            this.ErrorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ErrorPanel.Location = new System.Drawing.Point(20, 82);
            this.ErrorPanel.Name = "ErrorPanel";
            this.ErrorPanel.Size = new System.Drawing.Size(337, 50);
            this.ErrorPanel.TabIndex = 4;
            this.ErrorPanel.Visible = false;
            // 
            // NewModuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 187);
            this.Controls.Add(this.ErrorPanel);
            this.Controls.Add(this.TypeBox);
            this.Controls.Add(this.LoadingPhaseBox);
            this.Controls.Add(this.CreateModuleButton);
            this.Controls.Add(this.ModuleNameBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewModuleForm";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add new module";
            this.Load += new System.EventHandler(this.NewModuleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ModuleNameBox;
        private System.Windows.Forms.Button CreateModuleButton;
        private System.Windows.Forms.ComboBox LoadingPhaseBox;
        private System.Windows.Forms.ComboBox TypeBox;
        private System.Windows.Forms.Panel ErrorPanel;
    }
}