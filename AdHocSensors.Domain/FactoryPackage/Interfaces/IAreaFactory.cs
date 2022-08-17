namespace AdHocSensors.Domain.FactoryPackage
{
    public interface IAreaFactory
    {
        Area Build();
        IPoisFactory WithPois();
        ISensorsFactory WithSensors();
    }
}