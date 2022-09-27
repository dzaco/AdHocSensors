using AdHocSensors.Domain;
using AdHocSensors.Domain.FactoryPackage;
using AdHocSensors.Domain.SettingsPackage;
using AdHocSensors.WpfApp.AreaComponent;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AdHocSensors
{
    public static class Register
    {
        private static Dictionary<Type, object> _registers = new Dictionary<Type, object>();
        public static IUnityContainer Container { get; set; }

        public static void InitrRegister()
        {
            Container = new UnityContainer();
            var area = CreateDefaultArea();
            Settings.Current.Area = area;
            Container.RegisterInstance<Area>(area);
            Container.RegisterInstance<AreaViewModel>(new AreaViewModel(area));
        }

        private static Area CreateDefaultArea()
        {
            var settings = Settings.Current;
            return new AreaFactory()
                .WithPois().EvenlyLocated(settings.AreaSize, settings.PoiCount).Then()
                .WithSensors().RandomLocated(settings.AreaSize, settings.SensorCount).Then()
                .Build();
        }
    }
}