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

        public void Turn(object value)
        {
            switch (value)
            {
                case string:
                    IsOn = (string)value == "1" || (string)value == "on";
                    break;

                case char:
                    IsOn = (char)value == '1';
                    break;

                case int:
                    IsOn = (int)value == 1;
                    break;

                case bool:
                    IsOn = (bool)value;
                    break;
            }
        }
    }
}