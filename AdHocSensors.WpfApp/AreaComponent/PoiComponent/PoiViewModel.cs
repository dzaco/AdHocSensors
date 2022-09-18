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

namespace AdHocSensors.WpfApp.AreaComponent.PoiComponent
{
    public class PoiViewModel : ShapeableViewModelBase
    {
        public Poi Poi => element as Poi;

        public PoiViewModel(Poi poi) : base(poi)
        {
        }

        protected override void CreateShape()
        {
            Shape = new Ellipse();
            Shape.Width = 5;
            Shape.Height = 5;
            Shape.Stroke = Poi.IsCovered ? Brushes.Green : Brushes.Black;
            Shape.StrokeThickness = 2;
            RefreshShapeCoordinates();
            SetToolTip();
        }

        protected override void SetToolTip()
        {
            Shape.ToolTip = $"POI#{Id} ({X},{Y})";
        }
    }
}