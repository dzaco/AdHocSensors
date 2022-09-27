using AdHocSensors.Domain;
using AdHocSensors.Domain.FactoryPackage;
using AdHocSensors.Domain.GA;
using AdHocSensors.Domain.SettingsPackage;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdHocSensors.DomainTests.GA
{
    internal class IndividualTests
    {
        [Test]
        public void ShouldRandStates()
        {
            var sensors = CreateSensors(count: 10);
            var ind = new Individual(sensors, 10).RandomStateTable();

            for (var row = 0; row < sensors.Count; row++)
            {
                for (var col = 0; col < 10; col++)
                {
                    var state = ind.StateTable.Get(row, col) ? "1" : "0";
                    Console.Write($"{state} ");
                }
                Console.WriteLine();
            }
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