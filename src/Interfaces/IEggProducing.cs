namespace Trestlebridge.Interfaces
{
    public interface IEggProducing
    {
        int EggsProduced { get; }
        int Gather();
    }
}