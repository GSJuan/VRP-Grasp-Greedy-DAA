using System;
using System.Collections.Generic;
using System.IO;

namespace Vehicle_Routing_Problem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string folderPath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"input\"); // C:\Users\Juan\source\repos\VRP-Grasp-Greedy-DAA\Vehicle Routing Problem\input\

            TableDrawing table = new TableDrawing(50);
            foreach (string filePath in Directory.GetFiles(folderPath, "*.txt"))
            {
                Problem problema = new Problem(filePath);
                Greedy greedy = new Greedy();
                
                Solution result = greedy.Solve(problema);
                int cost = result.getCost();

                for (int i = 0; i < result.getRoutes().Count; i++)
                {
                    Console.WriteLine("Route " + i + " With length " + result.getRoutes()[i].Count + " (counting both zeros) : ");
                    for (int j = 0; j < result.getRoutes()[i].Count; j++)
                    {
                        Console.Write(result.getRoutes()[i][j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Total Cost: " + cost);
                table.PrintLine();

            }
            
        }
    }
}
