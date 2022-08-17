using AdHocSensors.Common.Files;
using AdHocSensors.WpfApp.AreaComponent;
using AdHocSensors.WpfApp.SettingsViews;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AdHocSensors.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AreaViewModel AreaViewModel { get; set; }

        public MainWindow()
        {
            this.AreaViewModel = new AreaViewModel();
            InitializeComponent();
            this.DataContext = this;
        }

        private void OpenSettingsWindow(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow();
            window.Show();
        }

        private void LoadState(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == true)
            {
                var sensors = SensorFileReader.Read(dialog.FileName);
                this.AreaViewModel.Sensors = sensors;
            }
        }
    }
}