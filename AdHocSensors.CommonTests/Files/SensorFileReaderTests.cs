using AdHocSensors.Common.Files;
using AdHocSensors.Domain.SettingsPackage;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Settings.Default.Range = -1;
            Settings.Default.Scale = 1;
            double actualRange = SensorFileReader.GetSensorRangeFromPath(path);
            Assert.AreEqual(expected, actualRange);
        }

        [Test]
        public void ShouldThrowExceptionWhenFileNotExists()
        {
            var path = "invalid_path.txt";
            Assert.Throws<FileNotFoundException>(() => SensorFileReader.Read(path));
        }
    }
}