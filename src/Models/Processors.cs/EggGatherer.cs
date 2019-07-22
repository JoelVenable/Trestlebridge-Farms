namespace Trestlebridge.Models.Processors
{
    public class EggGatherer
    {
        int ChickenEggs { get; set; } = 0;
        int DuckEggs { get; set; } = 0;
        int OstrichEggs { get; set; } = 0;
        double TotalEggs
        {
            get
            {
                return ChickenEggs + DuckEggs + OstrichEggs;
            }
        }



    }
}
