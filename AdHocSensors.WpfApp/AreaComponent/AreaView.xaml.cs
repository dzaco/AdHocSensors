using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Windows;
using System.Windows.Controls;
using Unity;

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
        }

        public static readonly DependencyProperty AreaViewModelProperty =
        DependencyProperty.RegisterAttached(
            name: "AreaViewModel",
            propertyType: typeof(AreaViewModel),
            ownerType: typeof(AreaView),
            defaultMetadata: new PropertyMetadata(null, AreaViewModelChanged));

        private static void AreaViewModelChanged(DependencyObject dependency, DependencyPropertyChangedEventArgs eventArgs)
        {
            var view = dependency as AreaView;
            var viewModel = eventArgs.NewValue as AreaViewModel;
            view.AreaViewModel = viewModel;
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
                this.Border.Width = AreaViewModel.Size;
                this.Border.Height = AreaViewModel.Size;
                AreaViewModel.Build();
            }
        }

        public void Build()
        {
            //this.AreaViewModel.Canvas = this.AreaCanvas;
            SetSize();
            this.AreaViewModel.Build();
        }

        private void SetSize(object? sender = null, EventArgs e = null)
        {
            if (AreaViewModel != null)
            {
                this.Border.Width = AreaViewModel.Size;
                this.Border.Height = AreaViewModel.Size;
                this.AreaCanvas.Width = AreaViewModel.Size;
                this.AreaCanvas.Height = AreaViewModel.Size;
            }
        }
    }
}