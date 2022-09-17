namespace AdHocSensors.Domain.FactoryPackage
{
    public interface IPoisFactory
    {
        IPoisFactory RandomLocated(int size, int count);

        IPoisFactory EvenlyLocated(int size, int count);

        IAreaFactory Then();
    }
}