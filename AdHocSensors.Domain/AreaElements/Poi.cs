namespace AdHocSensors.Domain
{
    public class Poi : AreaElementBase
    {
        public Poi(int id, double x, double y) : base(id, x, y)
        {
        }

        public bool IsCovered { get; internal set; }
    }
}