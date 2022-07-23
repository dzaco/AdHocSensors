using AdHocSensors.Common.Commands;
using AdHocSensors.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.CommonTests.Commands
{
    [TestFixture]
    internal class AssignCommandTests
    {
        [Test]
        public void ShouldAssignIdToSensor()
        {
            var id = 10;
            var sensor = new Sensor(0, 0, 0, 0, 0);

            var command = new AssignCommand((obj, val) => (obj as Sensor).Id = (int)val);
            command.Execute(sensor, id);

            Assert.That(sensor.Id, Is.EqualTo(id));
        }
    }
}