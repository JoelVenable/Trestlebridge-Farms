using System;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Models.Animals
{
    public class Goat : IResource, IGrazing, IMeatProducing, IComposting
    {

        private Guid _id = Guid.NewGuid();
        private double _compostProduced = 7.5;

        private string _shortId
        {
            get
            {
                return this._id.ToString().Substring(this._id.ToString().Length - 6);
            }
        }

        public double GrassPerDay { get; set; } = 4.1;
        public string Type { get; } = "Goat";

        // Methods
        public void Graze()
        {
            Console.WriteLine($"Goat {this._shortId} just ate {this.GrassPerDay}kg of grass");
        }

        public double Butcher()
        {
            return _compostProduced;
        }

        public double Compost()
        {
            return _compostProduced;
        }


        public override string ToString()
        {
            return "goat";
        }
    }
}