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
