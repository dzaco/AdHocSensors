namespace AdHocSensors.Domain.GA
{
    internal class RouletteWheel
    {
        private Random random;
        private List<Individual> individuals;
        private Dictionary<Segment, Individual> fitnessDict;
        private double lastEndSegment;

        public RouletteWheel(List<Individual> individuals)
        {
            this.random = new Random();
            this.individuals = individuals;
            this.fitnessDict = new Dictionary<Segment, Individual>();
            Build();
        }

        private void Build()
        {
            this.lastEndSegment = 0.0;
            var sorted = this.individuals.OrderByDescending(ind => ind.CalculateFitness());
            foreach (var ind in sorted)
            {
                var newLast = lastEndSegment + ind.CalculateFitness();
                var segment = new Segment(from: lastEndSegment, to: newLast);
                this.fitnessDict.Add(segment, ind);
                lastEndSegment = newLast;
            }
        }

        internal Individual Rand()
        {
            var rand = random.NextDouble() * this.lastEndSegment;
            foreach (var d in fitnessDict)
            {
                var segment = d.Key;
                if (segment.From <= rand && rand < segment.To)
                {
                    return d.Value;
                }
            }
            return null;
        }
    }

    internal struct Segment
    {
        public double From;
        public double To;

        public Segment(double from, double to)
        {
            From = from;
            To = to;
        }
    }
}