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
        private RelayCommand resetAndCloseCommand;

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

        public RelayCommand ResetAndCloseCommand
        {
            get
            {
                if (resetAndCloseCommand == null)
                    resetAndCloseCommand = new RelayCommand((x) => this.ResetAndClose(x));
                return resetAndCloseCommand;
            }
        }

        private void SaveAndClose(object o)
        {
            this.Save();
            this.CloseWindow(o);
        }

        private void Save()
        {
            Settings.Current.CopyFrom(Settings.Editor);
        }

        private void ResetAndClose(object o)
        {
            this.ResetEditor();
            this.CloseWindow(o);
        }

        private void ResetEditor()
        {
            Settings.Editor.CopyFrom(Settings.Current);
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

        private RelayCommand testCommand;

        public RelayCommand TestCommand
        {
            get
            {
                if (testCommand == null)
                    testCommand = new RelayCommand((x) => MessageBox.Show($"POI count: {Settings.Editor.PoiCount}"));
                return testCommand;
            }
        }
    }
}