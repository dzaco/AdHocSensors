using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain.GA
{
    public sealed class Population
    {
        private readonly int individualCount;
        private readonly int chromosomLength;
        private Settings Settings;

        public int Generation { get; private set; }
        public List<Individual> Individuals { get; private set; }

        private Random random;

        public static Population RandomPopulation(int individualCount, int chromosomLength, List<Sensor> sensors)
        {
            var population = new Population(0, individualCount, chromosomLength);
            population.Individuals = RandIndividuals(individualCount, chromosomLength, sensors);
            return population;
        }

        private Population(int generation, int individualCount, int chromosomLength)
        {
            Settings = Settings.Current;
            this.Generation = generation;
            this.individualCount = individualCount;
            this.chromosomLength = chromosomLength;
            this.Individuals = new List<Individual>(chromosomLength);
            this.random = new Random();
        }

        private static List<Individual> RandIndividuals(int individualCount, int chromosomLength, List<Sensor> sensors)
        {
            var individuals = new List<Individual>(individualCount);
            for (var i = 0; i < individualCount; i++)
            {
                individuals.Add(new Individual(sensors, chromosomLength).RandomStateTable());
            }
            return individuals;
        }

        public Population NextGeneration()
        {
            var newPopulation = new Population(Generation + 1, individualCount, chromosomLength);
            var rouletteWheel = new RouletteWheel(this.Individuals);
            while (newPopulation.Individuals.Count < individualCount)
            {
                var pair = Selection(rouletteWheel);
                pair = Crossover(pair);
                Mutation(pair.Item1);
                Mutation(pair.Item2);
                Accept(pair, newPopulation);
            }
            return newPopulation;
        }

        private Tuple<Individual, Individual> Selection(RouletteWheel rouletteWheel)
        {
            return new Tuple<Individual, Individual>(rouletteWheel.Rand(), rouletteWheel.Rand());
        }

        private Tuple<Individual, Individual> Crossover(Tuple<Individual, Individual> pair)
        {
            var splitIndex = random.Next(chromosomLength);
            return pair.Item1.Crossover(pair.Item2, splitIndex);
        }

        private void Mutation(Individual individual)
        {
            if (random.NextDouble() < 0.03)
            {
                var index = random.Next(chromosomLength);
                individual.Mutate(index);
            }
        }

        private void Accept(Tuple<Individual, Individual> pair, Population newPopulation)
        {
            newPopulation.Individuals.Add(pair.Item1);
            newPopulation.Individuals.Add(pair.Item2);
        }

        public GAResult CreateResult()
        {
            return new GAResult(Generation, Individuals);
        }
    }
}