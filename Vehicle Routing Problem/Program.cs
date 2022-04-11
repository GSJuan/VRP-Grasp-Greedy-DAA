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
            TableDrawing bigTable = new TableDrawing(100);

            foreach (string filePath in Directory.GetFiles(folderPath, "*.txt"))
            {
                Problem problema = new Problem(filePath);
                
                Greedy greedy = new Greedy();
                Solution greedyResult = greedy.Solve(problema);
                int greedyCost = greedyResult.getCost();               

                for (int i = 0; i < greedyResult.getRoutes().Count; i++)
                {
                    Console.WriteLine("Route " + i + " With length " + greedyResult.getRoutes()[i].Count + " (counting both zeros) : ");
                    for (int j = 0; j < greedyResult.getRoutes()[i].Count; j++)
                    {
                        Console.Write(greedyResult.getRoutes()[i][j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Total Greedy Cost: " + greedyCost);
                                               
                table.PrintLine();

                Grasp grasp = new Grasp();
                Solution graspResult = grasp.Solve(problema);
                int graspCost = graspResult.getCost();

                for (int i = 0; i < graspResult.getRoutes().Count; i++)
                {
                    Console.WriteLine("Route " + i + " With length " + graspResult.getRoutes()[i].Count + " (counting both zeros) : ");
                    for (int j = 0; j < graspResult.getRoutes()[i].Count; j++)
                    {
                        Console.Write(graspResult.getRoutes()[i][j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Total Greedy Cost: " + graspCost);

                bigTable.PrintLine();

            }
            
        }
    }
}
