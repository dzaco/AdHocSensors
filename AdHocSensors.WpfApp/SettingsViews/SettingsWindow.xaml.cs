using AdHocSensors.Common.Commands;
using AdHocSensors.Domain.SettingsPackage;
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
using System.Windows.Shapes;

namespace AdHocSensors.WpfApp.SettingsViews
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private RelayCommand saveAndCloseCommand;
        private RelayCommand closeWindowCommand;

        public SettingsWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public RelayCommand SaveAndCloseCommand
        {
            get
            {
                if (saveAndCloseCommand == null)
                    saveAndCloseCommand = new RelayCommand((x) => this.SaveAndClose(x));
                return saveAndCloseCommand;
            }
        }

        public RelayCommand CloseWindowCommand
        {
            get
            {
                if (closeWindowCommand == null)
                    closeWindowCommand = new RelayCommand((x) => this.CloseWindow(x));
                return closeWindowCommand;
            }
        }

        private void SaveAndClose(object o)
        {
            this.Save();
            this.CloseWindow(o);
        }

        private void Save()
        {
        }

        private void CloseWindow(object o)
        {
            try
            {
                ((Window)o).Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}