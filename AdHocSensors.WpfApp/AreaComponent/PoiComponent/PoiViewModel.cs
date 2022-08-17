using AdHocSensors.Domain;
using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdHocSensors.WpfApp.AreaComponent.PoiComponent
{
    public class PoiViewModel
    {
        public PoiViewModel(Poi poi)
        {
            this.poi = poi;
            CreateShape();
            Settings.Current.ScaleChanged += RefreshShapeCoordinates;
        }

        private Poi poi;

        public int Id
        {
            get { return poi.Id; }
            set { poi.Id = value; }
        }

        public double X
        {
            get { return poi.X * Settings.Current.Scale; }
            set
            {
                poi.X = value / Settings.Current.Scale;
                _shape.Margin = new Thickness(value, Y, 0, 0);
            }
        }

        public double Y
        {
            get { return poi.Y * Settings.Current.Scale; }
            set
            {
                poi.Y = value / Settings.Current.Scale;
                _shape.Margin = new Thickness(X, value, 0, 0);
            }
        }

        private Shape _shape;

        public Shape Shape
        {
            get
            {
                return _shape;
            }
        }

        private void CreateShape()
        {
            _shape = new Ellipse();
            _shape.Width = 5;
            _shape.Height = 5;
            _shape.Stroke = Brushes.Black;
            _shape.StrokeThickness = 2;
            RefreshShapeCoordinates();
            SetToolTip();
        }

        private void SetToolTip()
        {
            _shape.ToolTip = $"#{poi.Id} ({poi.X},{poi.Y})";
        }

        public void RefreshShapeCoordinates(object? sender = null, EventArgs e = null)
        {
            _shape.Margin = new Thickness(X, Y, 0, 0);
        }
    }
}