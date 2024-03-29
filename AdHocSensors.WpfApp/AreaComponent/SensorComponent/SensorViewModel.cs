﻿using AdHocSensors.Domain;
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
            if (Sensor.Battery.IsOn)
                Shape = new Ellipse()
                {
                    Stroke = Brushes.Green,
                    Fill = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0)),
                    StrokeThickness = 2,
                };
            else
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
            var builder = new StringBuilder("Sensor #");
            builder.Append(Id);
            if (Sensor.Battery.IsOn)
                builder.Append(" ON");
            else
                builder.Append(" OFF");
            builder
                .Append(" q=\'").Append(Sensor.Coverage.ToString("0.00")).Append("\'\n(")
                .Append(Sensor.X.ToString("0.00"))
                .Append(", ")
                .Append(Sensor.Y.ToString("0.00"))
                .Append(")");
            Shape.ToolTip = builder.ToString();
        }

        private void RefreshShapeRange(object? sender = null, EventArgs e = null)
        {
            var size = Sensor.Range * Settings.Current.Scale * 2;
            Shape.Width = size;
            Shape.Height = size;
        }
    }
}