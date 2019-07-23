namespace Trestlebridge.Models.Processors
{
    public class EggGatherer
    {
        int ChickenEggs { get; set; } = 0;
        int DuckEggs { get; set; } = 0;
        int OstrichEggs { get; set; } = 0;

        public int Capacity { get; } = 5;  // rows of plants

        double TotalEggs
        {
            get
            {
                return ChickenEggs + DuckEggs + OstrichEggs;
            }
        }



    }
}
