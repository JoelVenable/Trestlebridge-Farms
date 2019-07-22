namespace Trestlebridge.Interfaces
{
    public interface ISeedProducing
    {
        string Type { get; }
        int Harvest();
    }
}