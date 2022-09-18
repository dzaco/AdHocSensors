using AdHocSensors.Domain.FactoryPackage;
using AdHocSensors.Domain.SettingsPackage;
using NUnit.Framework;
using System.Linq;

namespace AdHocSensors.DomainTests
{
    public class AreaFactoryTests
    {
        [Test]
        public void ShouldBuildEmptyArea()
        {
            var area = new AreaFactory().Build();

            Assert.That(area, Is.Not.Null);
            Assert.That(area.Pois, Is.Empty);
            Assert.That(area.Sensors, Is.Empty);
        }

        [Test]
        public void ShouldBuildWithRandomPois()
        {
            var area = new AreaFactory()
                .WithPois()
                    .RandomLocated(size: 100, count: Settings.Current.PoiCount)
                    .Then()
                .Build();

            Assert.That(area, Is.Not.Null);
            Assert.That(area.Pois, Is.Not.Empty.And.Count.EqualTo(Settings.Current.PoiCount));
            Assert.That(area.Pois, Is.Unique);

            Assert.That(area.Sensors, Is.Empty);
        }

        [Test]
        public void ShouldBuildWithPoisEvenlyDistributedOnTheArea()
        {
            var area = new AreaFactory()
                .WithPois()
                    .EvenlyLocated(size: 100, count: 121)
                    .Then()
                .Build();

            Assert.That(area, Is.Not.Null);
            Assert.That(area.Sensors, Is.Empty);
            Assert.That(area.Pois, Is.Not.Empty.And.Count.EqualTo(121));
            Assert.That(area.Pois, Is.Unique);

            // all pois = 121 => pois in row = 11
            // first = Pois[0] => last in row = Pois[10]
            // first in sec row = Pois[11]
            var first = area.Pois[0];
            var onRight = area.Pois[1];
            var secondOnRight = area.Pois[2];
            var onBelow = area.Pois[11];

            Assert.That(first.DistanceTo(onRight), Is.EqualTo(first.DistanceTo(onBelow)));
            Assert.That(first.DistanceTo(secondOnRight), Is.EqualTo(2 * first.DistanceTo(onRight)));
        }

        [Test]
        public void ShouldbuildWithRandomSensors()
        {
            var area = new AreaFactory()
                .WithSensors()
                    .RandomLocated(size: 100, count: Settings.Current.SensorCount)
                    .Then()
                .Build();

            Assert.That(area, Is.Not.Null);
            Assert.That(area.Pois, Is.Empty);
            Assert.That(area.Sensors, Is.Not.Empty.And.Count.EqualTo(Settings.Current.SensorCount));
            Assert.That(area.Sensors, Is.Unique);
        }
    }
}