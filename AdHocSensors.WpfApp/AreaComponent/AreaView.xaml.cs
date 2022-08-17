using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace AdHocSensors.WpfApp.AreaComponent
{
    /// <summary>
    /// Interaction logic for AreaView.xaml
    /// </summary>
    public partial class AreaView : UserControl
    {
        public AreaView()
        {
            Settings.Current.ScaleChanged += SetSize;
            InitializeComponent();
            Build();
        }

        public static readonly DependencyProperty AreaViewModelProperty =
        DependencyProperty.Register(
            name: "AreaViewModel",
            propertyType: typeof(AreaViewModel),
            ownerType: typeof(AreaView),
            typeMetadata: new FrameworkPropertyMetadata(defaultValue: new AreaViewModel()));

        public AreaView(AreaViewModel vm) : this()
        {
            this.AreaViewModel = vm;
        }

        public AreaViewModel AreaViewModel
        {
            get
            {
                var vm = (AreaViewModel)GetValue(AreaViewModelProperty);
                vm.Canvas = this.AreaCanvas;
                return vm;
            }
            set
            {
                SetValue(AreaViewModelProperty, value);
                AreaViewModel.Canvas = this.AreaCanvas;
                this.Border.Width = AreaViewModel.Width;
                this.Border.Height = AreaViewModel.Height;
            }
        }

        public void Build()
        {
            this.AreaViewModel.Canvas = this.AreaCanvas;
            SetSize();
            this.AreaViewModel.Build();
        }

        private void SetSize(object? sender = null, EventArgs e = null)
        {
            this.Border.Width = Settings.Current.Scale;
            this.Border.Height = Settings.Current.Scale;
            this.AreaCanvas.Width = Settings.Current.Scale;
            this.AreaCanvas.Height = Settings.Current.Scale;
        }
    }
}