using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain.GA
{
    public sealed class Individual
    {
        private readonly int rows;
        private readonly List<Sensor> sensors;
        private readonly int cols;
        public StateTable StateTable { get; }
        public double Coverage { get; private set; }

        public Individual(List<Sensor> sensors, int iterationCount, StateTable stateTable)
        {
            this.sensors = sensors;
            this.rows = sensors.Count;
            this.cols = iterationCount;
            this.StateTable = stateTable;
            this.Coverage = CalculateFitness();
        }

        public Individual(List<Sensor> sensors, int iterationCount)
            : this(sensors, iterationCount, new StateTable(sensors.Count, iterationCount))
        { }

        public double CalculateFitness()
        {
            var result = new double[cols];
            for (var time = 0; time < cols; time++)
            {
                var cov = CalculateFitnessInTime(time);
                result[time] = cov;
            }
            return result.Sum();
        }

        private double CalculateFitnessInTime(int time)
        {
            var area = Settings.Current.Area;
            for (var sensorId = 0; sensorId < rows; sensorId++)
            {
                var isOn = StateTable.Get(sensorId, time);
                area.Sensors[sensorId].Battery.Turn(isOn);
            }
            area.DetectCoveredPois();
            return area.Coverage();
        }

        public Individual RandomStateTable()
        {
            var random = new Random();
            for (var sensorId = 0; sensorId < rows; sensorId++)
            {
                var batteryCappacity = Settings.Current.BatteryCapacity;
                for (var t = 0; t < cols; t++)
                {
                    var isOn = random.NextDouble() < 0.5;
                    if (isOn && batteryCappacity > 0)
                        StateTable.Set(sensorId, t, true);
                    else
                        StateTable.Set(sensorId, t, false);
                }
            }
            this.Coverage = CalculateFitness();
            return this;
        }

        public Tuple<Individual, Individual> Crossover(Individual item2, int splitIndex)
        {
            var crossoverStates = this.StateTable.CrossoverWith(item2.StateTable, splitIndex);
            return new Tuple<Individual, Individual>
                (new Individual(this.sensors, iterationCount: cols, crossoverStates.Item1),
                new Individual(this.sensors, iterationCount: cols, crossoverStates.Item2));
        }

        public void Mutate(int index)
        {
            this.StateTable.Toggle(index);
            this.Coverage = CalculateFitness();
        }
    }
}