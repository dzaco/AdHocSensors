using AdHocSensors.Domain;
using AdHocSensors.Domain.SettingsPackage;
using AdHocSensors.WpfApp.AreaComponent.SensorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace AdHocSensors.WpfApp.AreaComponent.Interfaces
{
    public abstract class ShapeableViewModelBase
    {
        protected AreaElementBase element;
        public Shape Shape { get; protected set; }

        public ShapeableViewModelBase(AreaElementBase areaElementBase)
        {
            element = areaElementBase;
            CreateShape();
            Settings.Current.ScaleChanged += RefreshShapeCoordinates;
        }

        public int Id
        {
            get { return element.Id; }
            set { element.Id = value; }
        }

        public double X
        {
            get { return element.X * Settings.Current.Scale; }
            set
            {
                element.X = value / Settings.Current.Scale;
                SetCoordinatesToShapeCenter(value, Y);
            }
        }

        public double Y
        {
            get { return element.Y * Settings.Current.Scale; }
            set
            {
                element.Y = value / Settings.Current.Scale;
                SetCoordinatesToShapeCenter(X, value);
            }
        }

        protected abstract void CreateShape();

        protected abstract void SetToolTip();

        public void RefreshShapeCoordinates(object? sender = null, EventArgs e = null)
        {
            if (Shape != null)
            {
                SetCoordinatesToShapeCenter(X, Y);
            }
        }

        private void SetCoordinatesToShapeCenter(double x, double y)
        {
            var offset = element.Range * Settings.Current.Scale;
            Shape.Margin = new Thickness(x - offset, y - offset, 0, 0);
        }
    }
}