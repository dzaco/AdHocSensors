using AdHocSensors.WpfApp.AreaComponent;
using AdHocSensors.WpfApp.SettingsViews;
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
    }
}