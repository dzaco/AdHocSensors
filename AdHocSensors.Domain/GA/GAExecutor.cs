using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain.GA
{
    public class GAExecutor
    {
        private Settings settings;

        public GAExecutor()
        {
            this.settings = Settings.Current;
        }

        public IEnumerable<GAResult> Execute()
        {
            var population = Population.RandomPopulation(settings.PopulationSize, settings.IterationCount, settings.Area.Sensors);
            while (population.Generation < settings.GenerationCount)
            {
                yield return population.CreateResult();
                population = population.NextGeneration();
            }
        }
    }
}