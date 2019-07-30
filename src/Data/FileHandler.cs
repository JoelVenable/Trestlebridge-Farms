using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Trestlebridge.Data
{
    public class FileHandler
    {
        static private string filePath = "data.txt";

        static private List<string> lines = null;

        private Dictionary<string, double> values = new Dictionary<string, double>();

        public List<string> Facilities { get; } = new List<string>();

        public FileHandler()
        {
            Update();
        }

        public void Update()
        {
            lines = File.ReadAllLines(filePath).ToList();
            values.Clear();

            lines.ForEach(line =>
            {
                List<string> entries = line.Split(":").ToList();
                if (entries.Count == 1)
                {
                    //  variable
                    Console.WriteLine(line);

                    entries = line.Split(",").ToList();
                    values.Add(entries[0], Double.Parse(entries[1]));
                }
                else
                {
                    Facilities.Add(line);
                }
            });

        }

        public void SaveData(List<string> data)
        {
            File.WriteAllLines(filePath, data);
        }

        public int GetData(string key, int num)
        {
            try
            {
                values.TryGetValue(key, out double value);
                return (int)value;
            }
            catch { return num; }
        }
        public double GetData(string key, double num)
        {
            try
            {
                values.TryGetValue(key, out double value);
                return value;

            }
            catch { return num; }
        }

    }
}
