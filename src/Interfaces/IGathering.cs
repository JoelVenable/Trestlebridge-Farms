using System.Collections.Generic;
using Trestlebridge.Models;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Interfaces
{
    public interface IGathering
    {
        string Name { get; }
        double Capacity { get; }

        int NumAnimals { get; }

        void SendToBasket(int num, Farm farm);
    }
}