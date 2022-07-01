namespace AdHocSensors.Domain.FactoryPackage
{
    public interface ISensorsFactory
    {
        ISensorsFactory RandomLocated(int count);

        IAreaFactory Then();
    }
}