namespace Trestlebridge.Models.Processors
{
    public class MeatProcessor
    {
        double ChickenMeat { get; set; } = 0;
        double CowMeat { get; set; } = 0;
        double PigMeat { get; set; } = 0;
        double SheepMeat { get; set; } = 0;
        double OstrichMeat { get; set; } = 0;

        public int Capacity { get; } = 5;  // rows of plants

        double TotalMeat
        {
            get
            {
                return ChickenMeat + CowMeat + PigMeat + SheepMeat + OstrichMeat;
            }
        }



    }
}
