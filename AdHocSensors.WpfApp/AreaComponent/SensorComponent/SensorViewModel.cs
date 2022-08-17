using AdHocSensors.Domain;
using AdHocSensors.Domain.SettingsPackage;
using AdHocSensors.WpfApp.AreaComponent.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdHocSensors.WpfApp.AreaComponent.SensorComponent
{
    public class SensorViewModel : ShapeableViewModelBase
    {
        public Sensor Sensor => element as Sensor;

        public SensorViewModel(Sensor sensor) : base(sensor)
        {
            Settings.Current.ScaleChanged += RefreshShapeRange;
        }

        protected override void CreateShape()
        {
            Shape = new Ellipse()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
            };
            RefreshShapeCoordinates();
            RefreshShapeRange();
            SetToolTip();
        }

        protected override void SetToolTip()
        {
            Shape.ToolTip = $"S#{Id} ({X},{Y})";
        }

        private void RefreshShapeRange(object? sender = null, EventArgs e = null)
        {
            var size = Sensor.Range * Settings.Current.Scale * 2;
            Shape.Width = size;
            Shape.Height = size;
        }
    }
}