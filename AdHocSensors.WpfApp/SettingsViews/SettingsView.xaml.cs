using AdHocSensors.Domain.SettingsPackage;
using AdHocSensors.WpfApp.SettingsViews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdHocSensors.WpfApp.SettingsViews
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private SettingsViewModel viewModel;

        public SettingsView()
        {
            viewModel = new SettingsViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}