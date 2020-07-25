using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnrealProjectTool
{
    public partial class ModuleView : UserControl
    {
        public ModuleView(ModuleData LoadedModuleData)
        {
            InitializeComponent();

            Data = LoadedModuleData;
            BuildView();
        }

        private void BuildView()
        {
            ModuleNameLabel.Text = ModuleNameLabel.Text + " " + Data.Name;
            ModuleTypeLabel.Text = ModuleTypeLabel.Text + " " + Data.Type;
            LoadingPhaseLabel.Text = LoadingPhaseLabel.Text + " " + Data.LoadingPhase;
        }

        ModuleData Data;
    }
}
