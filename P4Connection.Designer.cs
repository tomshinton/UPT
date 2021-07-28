namespace UnrealProjectTool
{
    partial class P4Connection
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
            this.UserBox = new System.Windows.Forms.TextBox();
            this.ServerLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.WorkspaceLabel = new System.Windows.Forms.Label();
            this.ServerBox = new System.Windows.Forms.TextBox();
            this.WorkspaceBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserBox
            // 
            this.UserBox.Location = new System.Drawing.Point(108, 43);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(314, 20);
            this.UserBox.TabIndex = 1;
            this.UserBox.TextChanged += new System.EventHandler(this.LoginInfoChanged);
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(23, 20);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(41, 13);
            this.ServerLabel.TabIndex = 1;
            this.ServerLabel.Text = "Server:";
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(23, 46);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(32, 13);
            this.UserLabel.TabIndex = 2;
            this.UserLabel.Text = "User:";
            // 
            // WorkspaceLabel
            // 
            this.WorkspaceLabel.AutoSize = true;
            this.WorkspaceLabel.Location = new System.Drawing.Point(23, 72);
            this.WorkspaceLabel.Name = "WorkspaceLabel";
            this.WorkspaceLabel.Size = new System.Drawing.Size(65, 13);
            this.WorkspaceLabel.TabIndex = 3;
            this.WorkspaceLabel.Text = "Workspace:";
            // 
            // ServerBox
            // 
            this.ServerBox.Location = new System.Drawing.Point(108, 17);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(314, 20);
            this.ServerBox.TabIndex = 0;
            this.ServerBox.TextChanged += new System.EventHandler(this.LoginInfoChanged);
            // 
            // WorkspaceBox
            // 
            this.WorkspaceBox.Location = new System.Drawing.Point(108, 69);
            this.WorkspaceBox.Name = "WorkspaceBox";
            this.WorkspaceBox.Size = new System.Drawing.Size(314, 20);
            this.WorkspaceBox.TabIndex = 2;
            this.WorkspaceBox.TextChanged += new System.EventHandler(this.LoginInfoChanged);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Enabled = false;
            this.ConnectButton.Location = new System.Drawing.Point(12, 159);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(447, 27);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ServerBox);
            this.panel1.Controls.Add(this.UserBox);
            this.panel1.Controls.Add(this.WorkspaceBox);
            this.panel1.Controls.Add(this.PasswordBox);
            this.panel1.Controls.Add(this.PasswordLabel);
            this.panel1.Controls.Add(this.WorkspaceLabel);
            this.panel1.Controls.Add(this.UserLabel);
            this.panel1.Controls.Add(this.ServerLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20);
            this.panel1.Size = new System.Drawing.Size(447, 141);
            this.panel1.TabIndex = 7;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(23, 99);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.PasswordLabel.TabIndex = 9;
            this.PasswordLabel.Text = "Password:";
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(108, 96);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new System.Drawing.Size(314, 20);
            this.PasswordBox.TabIndex = 4;
            // 
            // P4Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 199);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ConnectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "P4Connection";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to Perforce";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.P4Connection_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox UserBox;
        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label WorkspaceLabel;
        private System.Windows.Forms.TextBox ServerBox;
        private System.Windows.Forms.TextBox WorkspaceBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox PasswordBox;
    }
}