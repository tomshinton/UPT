namespace UnrealProjectTool
{
    partial class ModuleView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.LoadingPhaseLabel = new System.Windows.Forms.Label();
            this.ModuleTypeLabel = new System.Windows.Forms.Label();
            this.ModuleNameLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LoadingPhaseLabel);
            this.panel1.Controls.Add(this.ModuleTypeLabel);
            this.panel1.Controls.Add(this.ModuleNameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(20);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(465, 64);
            this.panel1.TabIndex = 0;
            // 
            // LoadingPhaseLabel
            // 
            this.LoadingPhaseLabel.AutoSize = true;
            this.LoadingPhaseLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LoadingPhaseLabel.Location = new System.Drawing.Point(10, 36);
            this.LoadingPhaseLabel.Name = "LoadingPhaseLabel";
            this.LoadingPhaseLabel.Size = new System.Drawing.Size(81, 13);
            this.LoadingPhaseLabel.TabIndex = 2;
            this.LoadingPhaseLabel.Text = "Loading Phase:";
            // 
            // ModuleTypeLabel
            // 
            this.ModuleTypeLabel.AutoSize = true;
            this.ModuleTypeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ModuleTypeLabel.Location = new System.Drawing.Point(10, 23);
            this.ModuleTypeLabel.Name = "ModuleTypeLabel";
            this.ModuleTypeLabel.Size = new System.Drawing.Size(34, 13);
            this.ModuleTypeLabel.TabIndex = 1;
            this.ModuleTypeLabel.Text = "Type:";
            // 
            // ModuleNameLabel
            // 
            this.ModuleNameLabel.AutoSize = true;
            this.ModuleNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ModuleNameLabel.Location = new System.Drawing.Point(10, 10);
            this.ModuleNameLabel.Name = "ModuleNameLabel";
            this.ModuleNameLabel.Size = new System.Drawing.Size(76, 13);
            this.ModuleNameLabel.TabIndex = 0;
            this.ModuleNameLabel.Text = "Module Name:";
            // 
            // ModuleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ModuleView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(485, 84);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ModuleTypeLabel;
        private System.Windows.Forms.Label ModuleNameLabel;
        private System.Windows.Forms.Label LoadingPhaseLabel;
    }
}
