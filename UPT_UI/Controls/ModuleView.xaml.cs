using System.Windows.Controls;

namespace UPT_UI.Controls
{
    public partial class ModuleView : UserControl
    {
        public ModuleView()
        {
            InitializeComponent();
        }

        public ModuleView(ModuleData InModuleData)
        {
            InitializeComponent();

            SourceData = InModuleData;
            UpdateTextFromData();
        }

        private void UpdateTextFromData()
        {
            ModuleNameText.Text = "Module Name: " + SourceData.Name;
            ModuleTypeText.Text = "Type: " + SourceData.Type;
            ModuleLoadingPhaseText.Text = "Load Phase: " + SourceData.LoadingPhase;
        }

        private ModuleData SourceData;
    }
}
