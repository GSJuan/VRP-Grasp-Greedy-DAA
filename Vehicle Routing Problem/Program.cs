using System;
using System.Diagnostics;
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
            TableDrawing bigTable = new TableDrawing(75);

            table.PrintRow("FileName", "Algorithm", "Cost", "Time in ms");

            Stopwatch timer = new Stopwatch();

            foreach (string filePath in Directory.GetFiles(folderPath, "*.txt"))
            {
                string[] name = filePath.Split("\\");
                Problem problema = new Problem(filePath);

                Greedy greedy = new Greedy();
                timer.Restart();
                Solution greedyResult = greedy.Solve(problema);
                timer.Stop();
                int greedyCost = greedyResult.getCost();

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

                Grasp grasp = new Grasp();
                timer.Restart();
                Solution graspResult = grasp.Solve(problema);
                timer.Stop();
                int graspCost = graspResult.getCost();

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
                //Console.WriteLine("Total Grasp Cost: " + graspCost);

                table.PrintRow(name[name.Length - 1], "GVNS", graspCost.ToString(), $"{timer.ElapsedMilliseconds}" );
                bigTable.PrintLine();

            }

        }
    }
}
