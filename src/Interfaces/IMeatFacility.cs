using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Models;


namespace Trestlebridge.Interfaces
{
    public interface IMeatFacility
    {
        List<IGrouping<string, IMeatProducing>> CreateMeatGroup();

        int NumMeatAnimals { get; }


        void SendToHopper(int numToProcess, string type, Farm farm);

    }
}