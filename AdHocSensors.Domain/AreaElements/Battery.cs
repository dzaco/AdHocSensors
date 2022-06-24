namespace AdHocSensors.Domain
{
    public class Battery
    {
        public Battery(double capacity)
        {
            Capacity = capacity;
            IsOn = false;
        }

        public double Capacity { get; private set; }
        public bool IsOn { get; private set; }
    }
}