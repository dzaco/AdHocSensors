﻿using AdHocSensors.Domain;
using AdHocSensors.Domain.FactoryPackage;
using AdHocSensors.Domain.SettingsPackage;
using AdHocSensors.WpfApp.AreaComponent.PoiComponent;
using AdHocSensors.WpfApp.AreaComponent.SensorComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace AdHocSensors.WpfApp.AreaComponent
{
    public class AreaViewModel
    {
        public Canvas Canvas { get; set; }
        public double Width => Settings.Current.Scale;
        public double Height => Settings.Current.Scale;

        public List<Sensor> Sensors
        {
            get { return area.Sensors; }
            set
            {
                area.Sensors = value;
                Build();
            }
        }

        private Area area;
        private List<PoiViewModel> pois;

        public AreaViewModel()
        {
            this.area = new AreaFactory()
                .WithPois()
                    .EvenlyLocated(Settings.Current.PoiCount)
                    .Then()
                .Build();

            area.Sensors.Add(new Sensor(1, 0.5, 0.5, 0.1, 100));
        }

        internal void Build()
        {
            this.Canvas.Children.Clear();
            AddPoisToCanvas();
            AddSensorsToCanvas();
        }

        private void AddPoisToCanvas()
        {
            pois = area.Pois.Select(poi => new PoiViewModel(poi)).ToList();
            pois.ForEach(poi => this.Canvas.Children.Add(poi.Shape));
        }

        private void AddSensorsToCanvas()
        {
            var viewModels = area.Sensors.Select(sensor => new SensorViewModel(sensor)).ToList();
            viewModels.ForEach(sensor => this.Canvas.Children.Add(sensor.Shape));
        }
    }
}