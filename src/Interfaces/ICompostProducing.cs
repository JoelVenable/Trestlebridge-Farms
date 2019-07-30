using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Models;

namespace Trestlebridge.Interfaces
{
    public interface ICompostProducing : IFacility
    {
        int CompostAmount { get; }

        void SendToComposter(int numToProcess, string type, Farm farm);

        List<IGrouping<string, IComposting>> CreateCompostList();
    }
}