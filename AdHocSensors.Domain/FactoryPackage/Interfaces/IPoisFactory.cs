namespace AdHocSensors.Domain.FactoryPackage
{
    public interface IPoisFactory
    {
        IPoisFactory RandomLocated(int count);

        IPoisFactory EvenlyLocated(int count);

        IAreaFactory Then();
    }
}