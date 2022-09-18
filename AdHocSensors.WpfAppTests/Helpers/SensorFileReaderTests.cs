using AdHocSensors.Domain.SettingsPackage;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestResourceManager;

namespace AdHocSensors.CommonTests.Files
{
    [TestFixture]
    internal class SensorFileReaderTests
    {
        [Test]
        [TestCase("WSN-45d-r30.txt", 30.0d)]
        [TestCase("WSN-45d-r30 (1).txt", 30.0d)]
        public void ShouldReturnRangeFromPath(string path, double expected)
        {
            Settings.Current.Range = -1;
            Settings.Current.Scale = 1;
            double actualRange = SensorFileReader.GetSensorRangeFromPath(path);
            Assert.AreEqual(expected, actualRange);
        }

        [Test]
        public void ShouldThrowExceptionWhenFileNotExists()
        {
            var path = "invalid_path.txt";
            Assert.Throws<FileNotFoundException>(() => SensorFileReader.Read(path));
        }

        [Test]
        [TestCase("sensors-states-10.txt", 5, 10)]
        [TestCase("sensors.txt", 5, 0)]
        public void ShouldLoadSensorsWithIdAndStates(string fileName, int sensorCount, int decimalState)
        {
            var manager = new ResourceManager();
            var path = manager.GetFile(fileName);
            var sensors = SensorFileReader.Read(path);

            Assert.That(sensors, Is.Not.Null);
            Assert.That(sensors.Count, Is.EqualTo(sensorCount));

            var binaryStringStates = Convert.ToString(decimalState, 2)
                .PadLeft(sensorCount, '0')
                .Reverse();
            for (int i = 0; i < sensorCount; i++)
            {
                var stateString = binaryStringStates.ElementAt(i);
                var isOn = stateString == '1';
                var sensor = sensors[i];
                Assert.That(sensor.Battery.IsOn, Is.EqualTo(isOn));
            }

            Assert.That(sensors.GroupBy(sensor => sensor.Id).Count(), Is.EqualTo(sensorCount),
                "because all sensors should have unique id");
            Assert.That(sensors.GroupBy(sensor => new { sensor.X, sensor.Y }).Count(), Is.GreaterThan(1),
                "because all sensors shouldn't have the same coordinates");
        }
    }
}