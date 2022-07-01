using AdHocSensors.Domain;
using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdHocSensors.Common.Files
{
    public class SensorFileReader
    {
        public static List<Sensor> Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            var sensorList = new List<Sensor>();
            var sensorId = 0;
            var range = GetSensorRangeFromPath(path);
            var batteryCapacity = Settings.Default.BatteryCapacity;

            var lines = File.ReadAllLines(path);
            var lineNumber = 0;
            foreach (var line in lines)
            {
                lineNumber++;
                if (line.StartsWith('#'))
                    continue;

                var coordinateStrArr = line.Split(' ');
                if (coordinateStrArr.Count() != 2)
                    throw new ArgumentOutOfRangeException($"Cannot parse line number {lineNumber}: \'{line}\'. Incorrect number of coordinates");

                var coordinates = coordinateStrArr.Select(x =>
                {
                    if (!double.TryParse(x, out var coordinate))
                        throw new ArgumentException($"Cannot parse line number {lineNumber}: \'{line}\'. Cannot parse to double");
                    return coordinate;
                });

                sensorList.Add(new Sensor(sensorId++,
                    coordinates.ElementAt(0), coordinates.ElementAt(1),
                    range, batteryCapacity));
            }
            return sensorList;
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