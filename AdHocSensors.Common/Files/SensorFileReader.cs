using AdHocSensors.Common.Commands;
using AdHocSensors.Domain;
using AdHocSensors.Domain.FactoryPackage;
using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdHocSensors.Common.Files
{
    public class SensorFileReader
    {
        public static List<Sensor> Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            var sensorList = new List<Sensor>();
            var range = GetSensorRangeFromPath(path);
            var batteryCapacity = Settings.Default.BatteryCapacity;

            var lines = File.ReadAllLines(path);
            var lineNumber = 0;
            var assignCommands = new List<AssignCommand>();

            foreach (var line in lines)
            {
                lineNumber++;
                if (line.StartsWith('#'))
                {
                    assignCommands = CreateCommandList(line);
                    continue;
                }

                var sensor = new Sensor(0, 0, 0, range, Settings.Default.BatteryCapacity);
                var parameters = line.Split(' ');
                for (int i = 0; i < assignCommands.Count; i++)
                {
                    var command = assignCommands[i];
                    var parameter = parameters[i];
                    command.Execute(sensor, parameter);
                }

                sensorList.Add(sensor);
            }

            if (sensorList.TrueForAll(sensor => sensor.Id == 0))
            {
                var sensorId = 0;
                sensorList.ForEach(sensor => sensor.Id = sensorId++);
            }
            return sensorList;
        }

        private static List<AssignCommand> CreateCommandList(string line)
        {
            if (line.StartsWith("#"))
                line = line.Substring(1);

            line.Trim();
            var list = new List<AssignCommand>();
            foreach (var parameter in line.Split(' '))
            {
                switch (parameter)
                {
                    case "id":
                        list.Add(new AssignCommand((obj, value) => (obj as Sensor).Id = int.Parse((string)value)));
                        break;

                    case "x":
                        list.Add(new AssignCommand((obj, value) => (obj as Sensor).X = double.Parse((string)value)));
                        break;

                    case "y":
                        list.Add(new AssignCommand((obj, value) => (obj as Sensor).Y = double.Parse((string)value)));
                        break;

                    case "state":
                        list.Add(new AssignCommand((obj, value) => (obj as Sensor).Battery.Turn((string)value)));
                        break;
                }
            }

            return list;
        }

        public static double GetSensorRangeFromPath(string path)
        {
            try
            {
                var regex = new Regex("WSN-[0-9]+d-r[0-9]+");
                var match = regex.Match(path);
                if (match.Success)
                {
                    var startIndex = match.Value.IndexOf("-r") + "-r".Length;
                    var searchArea = match.Value.Substring(startIndex);
                    var rangeString = searchArea.TakeWhile(c => Char.IsDigit(c));
                    var fileDeclerRange = double.Parse(String.Join("", rangeString));
                    return fileDeclerRange / Settings.Default.Scale;
                }
                else
                    throw new ArgumentException();
            }
            catch (Exception)
            {
                return Settings.Default.Range;
            }
        }
    }
}