using AdHocSensors.Common.Commands;
using AdHocSensors.Domain;
using AdHocSensors.Domain.FactoryPackage;
using AdHocSensors.Domain.SettingsPackage;
using AdHocSensors.WpfApp.AreaComponent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace AdHocSensors.WpfApp.Commands
{
    public class ApplyNewSettingsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            var editor = parameter as Settings;
            var settings = Settings.Current;
            return editor != null && !settings.Equals(editor);
        }

        public void Execute(object? parameter)
        {
            var editor = parameter as Settings;
            var settings = Settings.Current;
            var needUpdateSize = settings.AreaSize != editor.AreaSize || settings.Scale != editor.Scale;
            var changesCommands = CreateCommandList(settings, editor);

            settings.CopyFrom(editor);

            if (needUpdateSize)
                Settings.Current.EmitSizeChanged();
            UpdateArea(changesCommands);
        }

        private List<RelayCommand> CreateCommandList(Settings settings, Settings? editor)
        {
            var list = new List<RelayCommand>();

            if (settings.SensorCount != editor.SensorCount)
                list.Add(RandomSensorsCommand());
            if (settings.Range != editor.Range)
                list.Add(RangeCommand());
            return list;
        }

        private void UpdateArea(List<RelayCommand> commandList)
        {
            var area = Register.Container.Resolve<Area>();
            var areaVM = Register.Container.Resolve<AreaViewModel>();

            var factory = new AreaFactory(area);
            factory.WithPois().EvenlyLocated(Settings.Current.AreaSize, Settings.Current.PoiCount);

            foreach (var command in commandList)
                command.Execute(factory);

            area = factory.Build();
            areaVM.Build();
        }

        private RelayCommand RandomSensorsCommand()
        {
            return new RelayCommand(factory =>
                    ((AreaFactory)factory).WithSensors()
                    .RandomLocated(Settings.Current.AreaSize, Settings.Current.PoiCount));
        }

        private RelayCommand RangeCommand()
        {
            return new RelayCommand(factory =>
                    ((AreaFactory)factory).WithSensors()
                    .WithRange(Settings.Current.Range));
        }
    }
}