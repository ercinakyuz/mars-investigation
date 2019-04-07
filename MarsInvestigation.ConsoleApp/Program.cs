using System;
using System.Collections.Generic;
using MarsInvestigation.Core;

namespace MarsInvestigation.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandLines = new List<string>();
            var isExecutePressed = false;
            Console.WriteLine("Welcome to the Mars Command Center.");
            Console.WriteLine("Please enter your commands, leave empty for execution.");
            Console.WriteLine("------------------------------------------------------");
            while (!isExecutePressed)
            {
                var commandLine = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(commandLine))
                {
                    isExecutePressed = true;
                }
                else
                {
                    commandLines.Add(commandLine);
                }
            }

            var commandCenter = new CommandCenter();
            try
            {
                commandCenter.ExecuteCommands(commandLines.ToArray());
                Console.WriteLine("Here are the final rover states.");
                Console.WriteLine("---------------------------------");
                commandCenter.GetFinalRoverStates().ForEach(Console.WriteLine);
            }
            catch (Exception e)
            {
                Console.WriteLine("Command center throwed and exception, here are the details.");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine(e);
            }
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Press any key for terminate the program...");
            Console.ReadKey();

        }
    }
}
