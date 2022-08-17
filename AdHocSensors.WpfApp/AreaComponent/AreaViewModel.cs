using AdHocSensors.Domain;
using AdHocSensors.Domain.FactoryPackage;
using AdHocSensors.Domain.SettingsPackage;
using AdHocSensors.WpfApp.AreaComponent.PoiComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace AdHocSensors.WpfApp.AreaComponent
{
    public class AreaViewModel
    {
        public Canvas Canvas { get; set; }
        public double Width => Settings.Current.Scale;
        public double Height => Settings.Current.Scale;

        public AreaView View { get; internal set; }

        private Area area;
        private List<PoiViewModel> pois;

        public AreaViewModel()
        {
            this.area = new AreaFactory()
                .WithPois()
                    .EvenlyLocated(Settings.Current.PoiCount)
                    .Then()
                .Build();
        }

        internal void Build()
        {
            SetSize();
            FillCanvas();
        }

        private void SetSize()
        {
            this.View.Border.Width = Settings.Current.Scale;
            this.View.Border.Height = Settings.Current.Scale;
        }

        private void FillCanvas()
        {
            this.Canvas.Children.Clear();
            pois = area.Pois.Select(poi => new PoiViewModel(poi)).ToList();
            pois.ForEach(poi => this.Canvas.Children.Add(poi.Shape));
        }
    }
}