using System;
using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Actions;
using Trestlebridge.Models;

namespace Trestlebridge
{
    class StandardMessages
    {
        public static void DisplayBanner()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(@"
       +---------------------------------------+
       | T  R  E  S  T  L  E  B  R  I  D  G  E |
       +-----------+---------------+-----------+
                   | F  A  R  M  S |
                   +---------------+");
            Console.WriteLine();
        }

        public static void ShowMessage(string message)
        {
            DisplayBanner();
            Console.WriteLine(message);
            Console.ReadLine();
        }

        public static string GetInput(string message)
        {
            DisplayBanner();
            Console.WriteLine(message);
            Console.WriteLine();
            Console.Write("> ");
            return Console.ReadLine();
        }

        public static int GetNumber(string message, int maximumNumber)
        {
            DisplayBanner();
            Console.WriteLine(message);
            Console.WriteLine();
            Console.Write("> ");
            string response = Console.ReadLine();
            try
            {
                int output = Int32.Parse(response);
                if (output <= maximumNumber && output > 0) return output;
                else throw new Exception();
            }
            catch
            {
                ShowMessage("Invalid Selection.");
                return GetNumber(message, maximumNumber);
            }
        }

        public static bool GetYesOrNo(string message)
        {
            StandardMessages.DisplayBanner();
            Console.WriteLine(message);
            Console.WriteLine();
            Console.WriteLine("Please press (Y/y) or (N/n).");
            Console.Write("> ");
            string response = Console.ReadLine();
            switch (response)
            {
                case "Y":
                    return true;
                case "y":
                    return true;
                case "N":
                    return false;
                case "n":
                    return false;
                default:
                    ShowMessage("Invalid selection. Please try again.");
                    return GetYesOrNo(message);
            }
        }

        
        public static int ShowMenu(List<string> menuOptions, string messagePrompt, string quitMessage = "Go back...")
        {
            int output = 0;
            DisplayBanner();
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");
            }
            Console.WriteLine();
            Console.WriteLine($"0. {quitMessage}");
            Console.WriteLine();
            Console.WriteLine(messagePrompt);
            Console.WriteLine();
            Console.Write("> ");
            string selection = Console.ReadLine();

            try
            {
                output = Int32.Parse(selection);
                if (output > menuOptions.Count) throw new Exception();
            }
            catch
            {
                ShowMessage("Invalid selection.  Please try again.");
            }

            return output;
        }


    }
}
