using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnrealProjectTool
{
    public partial class NewModuleForm : Form
    {
        public delegate void OnNewModuleCreatedDelegate(ModuleData NewModuleData);
        public OnNewModuleCreatedDelegate OnNewModuleCreated;
        public NewModuleForm()
        { 
           InitializeComponent();
        }

        private void NewModuleForm_Load(object sender, EventArgs e)
        {
            DisableNewModuleButton();

            ModuleNameBox.Text = "Enter a new name for your module";
            ModuleNameBox.ForeColor = Color.Gray;
        }

        private void CreateModuleButton_Click(object sender, EventArgs e)
        {
            ModuleData NewModuleData = new ModuleData(ModuleNameBox.Text, TypeBox.Text, LoadingPhaseBox.Text);
            OnNewModuleCreated.Invoke(NewModuleData);

            Close();
        }

        private void ModuleNameBox_TextChanged(object sender, EventArgs e)
        {
            ModuleNameBox.ForeColor = Color.Black;
            ErrorPanel.Visible = true;
            ErrorPanel.Controls.Clear();

            if (NewModuleIsValid())
            {
                EnableNewModuleButton();
            }
            else
            {
                DisableNewModuleButton();
            }
        }

        private bool NewModuleIsValid()
        {
            bool ModuleIsValid = true;
            if(ModuleNameBox.Text.Contains(" "))
            {
                AddErrorMessage(@"Module cannot contain spaces");
                ModuleIsValid = false;
            }

            if(ModuleNameBox.Text == "")
            {
                AddErrorMessage(@"Cannot add a module with no name");
                ModuleIsValid = false;
            }

            foreach(ModuleData Module in CurrentProjectProxy.Modules)
            {
                if(Module.Name == ModuleNameBox.Text)
                {
                    AddErrorMessage(@"Module " + Module.Name + " already existing in project");
                    ModuleIsValid = false;
                }
            }

            return ModuleIsValid;
        }

        private void AddErrorMessage(string InError)
        {
            Label NewError = new Label();
            NewError.Text = InError;
            NewError.ForeColor = Color.Red;
            NewError.Dock = DockStyle.Top;

            ErrorPanel.Controls.Add(NewError);
        }
        private void DisableNewModuleButton()
        {
            CreateModuleButton.Enabled = false;
            CreateModuleButton.Text = "Cannot add module";
        }

        private void EnableNewModuleButton()
        {
            CreateModuleButton.Enabled = true;
            CreateModuleButton.Text = "Add " + ModuleNameBox.Text + " Module";
        }

       public UProjectProxy CurrentProjectProxy;
    }
}
