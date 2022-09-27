using AdHocSensors.Domain.GA;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.DomainTests.GA
{
    internal class StateTableTests
    {
        [Test]
        public void ShouldCrossoverTwoStateTable()
        {
            // Arrange
            var table1 = new StateTable(3, 3);
            table1.Set(1, 1, true);
            var table2 = new StateTable(3, 3);
            table2.Set(2, 2, true);

            // Act
            var crossoveredTables = table1.CrossoverWith(table2, 2);

            // Assert
            Assert.That(crossoveredTables.Item1.Get(1, 1), Is.EqualTo(true), "because it should be untouched");
            Assert.That(crossoveredTables.Item2.Get(1, 1), Is.EqualTo(false), "because it should be untouched");

            Assert.That(crossoveredTables.Item1.Get(2, 2), Is.EqualTo(true), "because it comes from table2");
            Assert.That(crossoveredTables.Item2.Get(2, 2), Is.EqualTo(false), "because it comes to table1");
        }

        [Test]
        public void ShouldToggleValuesInCol()
        {
            var table = new StateTable(3, 3);
            table.Toggle(1);
            Assert.That(table.GetSensorsInTime(1), Is.All.True);
        }
    }
}