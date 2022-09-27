using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain.GA
{
    public class StateTable
    {
        public double[] Coverage { get; }
        private bool[][] table;
        private int rows;
        private int cols;

        public StateTable(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;

            this.table = CreateTable(rows, cols);
            this.Coverage = new double[cols];
        }

        public bool[][] CreateTable(int rows, int cols)
        {
            var table = new bool[rows][];
            for (var row = 0; row < table.Length; row++)
                table[row] = new bool[cols];

            return table;
        }

        public bool Get(int sensorId, int time)
        {
            return this.table[sensorId][time];
        }

        public bool[] GetSensorsInTime(int time)
        {
            var sensorStates = new bool[this.rows];
            for (var sensorId = 0; sensorId < rows; sensorId++)
            {
                sensorStates[sensorId] = this.Get(sensorId, time);
            }
            return sensorStates;
        }

        public void Set(int sensorId, int time, bool value)
        {
            this.table[sensorId][time] = value;
        }

        public Tuple<StateTable, StateTable> CrossoverWith(StateTable otherTable, int splitIndex)
        {
            StateTable item1 = new StateTable(rows, cols);
            StateTable item2 = new StateTable(rows, cols);

            for (var sensorId = 0; sensorId < rows; sensorId++)
            {
                for (var time = 0; time < cols; time++)
                {
                    if (time < splitIndex)
                    {
                        item1.Set(sensorId, time, this.Get(sensorId, time));
                        item2.Set(sensorId, time, otherTable.Get(sensorId, time));
                    }
                    else
                    {
                        item1.Set(sensorId, time, otherTable.Get(sensorId, time));
                        item2.Set(sensorId, time, this.Get(sensorId, time));
                    }
                }
            }

            return new Tuple<StateTable, StateTable>(item1, item2);
        }

        public void Toggle(int index)
        {
            for (var sensorId = 0; sensorId < rows; sensorId++)
            {
                table[sensorId][index] = !table[sensorId][index];
            }
        }
    }
}