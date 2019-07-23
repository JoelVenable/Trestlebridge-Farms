using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Models;

namespace Trestlebridge.Interfaces
{
    public interface ICompostProducing
    {
        string Name { get; set; }
        int CompostAmmount { get; }

        void SendToHopper(int numToProcess, string type, Farm farm);

        List<IGrouping<string, IComposting>> CreateCompostList();
    }
}