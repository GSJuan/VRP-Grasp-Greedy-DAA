using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Vehicle_Routing_Problem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string folderPath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"input\"); // C:\Users\Juan\source\repos\VRP-GVNS-Greedy-DAA\Vehicle Routing Problem\input\

            TableDrawing table = new TableDrawing(100);
            TableDrawing bigTable = new TableDrawing(75);

            

            Stopwatch timer = new Stopwatch();

            foreach (string filePath in Directory.GetFiles(folderPath, "*.txt"))
            {
                table.PrintRow("Problem´s FileName", "Algorithm used", "Cost", "Time in ms");
                table.PrintLine();
                string[] name = filePath.Split("\\");
                Problem problema = new Problem(filePath);

                Greedy greedy = new Greedy();
                timer.Restart();
                Solution greedyResult = greedy.Solve(problema);
                timer.Stop();
                int greedyCost = greedyResult.GetCost();

                /*
                for (int i = 0; i < greedyResult.getRoutes().Count; i++)
                {
                    Console.WriteLine("Route " + i + " With length " + greedyResult.getRoutes()[i].Count + " (counting both zeros) : ");
                    for (int j = 0; j < greedyResult.getRoutes()[i].Count; j++)
                    {
                        Console.Write(greedyResult.getRoutes()[i][j] + " ");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.WriteLine("Total Greedy Cost: " + greedyCost);
                */

                table.PrintRow(name[name.Length - 1], "Greedy", greedyCost.ToString(), $"{timer.ElapsedMilliseconds}");
                table.PrintLine();

                List<EnvironmentStructure> structures = new List<EnvironmentStructure>
                {
                    new ReinsercionEntre(),
                    new ReinsercionIntra(),
                    new IntercambioEntre(),
                    new IntercambioIntra()
                };
                foreach (EnvironmentStructure structure in structures) {
                    Grasp grasp = new Grasp(structure);
                    timer.Restart();
                    Solution graspResult = grasp.Solve(problema);
                    timer.Stop();
                    int graspCost = graspResult.GetCost();

                    //int graspCalculatedCost = graspResult.calculateCost(ref problema.distanceMatrix);

                    //if (graspCalculatedCost == graspCost)
                    //{
                    //    Console.WriteLine("TRUE, " + graspCost.ToString() + " == " + graspCalculatedCost.ToString());
                    //}
                    //else
                    //{
                    //    Console.WriteLine("FALSE, " + graspCost.ToString() + " != " + graspCalculatedCost.ToString());
                    //}

                    //for (int i = 0; i < graspResult.getRoutes().Count; i++)
                    //{
                    //    Console.WriteLine("Route " + i + " With length " + graspResult.getRoutes()[i].Count + " (counting both zeros) : ");
                    //    for (int j = 0; j < graspResult.getRoutes()[i].Count; j++)
                    //    {
                    //        Console.Write(graspResult.getRoutes()[i][j] + " ");
                    //    }
                    //    Console.WriteLine();
                    //    Console.WriteLine();
                    //}
                    //Console.WriteLine("Total GVNS Cost: " + graspCost);
                    
                    table.PrintRow(name[name.Length - 1], "Grasp " + $"{structure.GetType().Name}", graspCost.ToString(), $"{timer.ElapsedMilliseconds}");
                    table.PrintLine();
                }

                GVNS gvns = new GVNS();
                timer.Restart();
                Solution gvnsResult = gvns.Solve(problema);
                timer.Stop();
                int gvnsCost = gvnsResult.GetCost();
                table.PrintRow(name[name.Length - 1], "GVNS", gvnsCost.ToString(), $"{timer.ElapsedMilliseconds}");
                
                table.PrintSeparatingLine();
                Console.WriteLine('\n');                
            }

        }
    }
}
