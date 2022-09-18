namespace AdHocSensors.Domain.FactoryPackage
{
    public interface ISensorsFactory
    {
        ISensorsFactory FromList(IEnumerable<Sensor> sensors);

        ISensorsFactory RandomLocated(int size, int count);

        IAreaFactory Then();

        ISensorsFactory WithRange(double range);
    }
}