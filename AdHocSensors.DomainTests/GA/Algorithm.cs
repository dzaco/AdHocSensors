using AdHocSensors.Domain.FactoryPackage;
using AdHocSensors.Domain;
using AdHocSensors.Domain.SettingsPackage;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdHocSensors.Domain.GA;

namespace AdHocSensors.DomainTests.GA
{
    internal class Algorithm
    {
        [Test]
        [TestCase(3)]
        public void ShouldReturnGAResults(int count)
        {
            Settings.Current.GenerationCount = count;
            Settings.Current.PopulationSize = count;
            Settings.Current.IterationCount = count;

            _ = CreateSensors(count * 2);
            var GA = new GAExecutor();
            var results = GA.Execute().ToList();
            results.ForEach(result => Console.WriteLine(result.ToString()));
            Assert.That(results.Count, Is.EqualTo(count));
        }

        private List<Sensor> CreateSensors(int count)
        {
            var area = new AreaFactory(Settings.Current.Area)
                .WithPois().EvenlyLocated(100, 121).Then()
                .WithSensors().RandomLocated(100, count).Then()
                .Build();
            area.DetectCoveredPois();
            Settings.Current.Area = area;
            return area.Sensors;
        }
    }
}