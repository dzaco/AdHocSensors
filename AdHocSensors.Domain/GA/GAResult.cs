using System.Globalization;

namespace AdHocSensors.Domain.GA
{
    public class GAResult
    {
        public GAResult(int generation, List<Individual> individuals)
        {
            Generation = generation;
            Individuals = individuals.OrderByDescending(i => i.Coverage).ToList();
            if (Individuals.Any())
            {
                Best = Individuals[0];
                Worst = Individuals[Individuals.Count - 1];
            }
        }

        public int Generation { get; }
        public List<Individual> Individuals { get; }
        public Individual Best { get; }
        public Individual Worst { get; }

        public override string ToString()
        {
            return
                $"G{Generation}\n" +
                $"Best : {Best.Coverage}\n" +
                $"Worst: {Worst.Coverage}";
        }
    }
}