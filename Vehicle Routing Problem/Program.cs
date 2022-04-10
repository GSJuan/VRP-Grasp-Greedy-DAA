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

            foreach (string filePath in Directory.GetFiles(folderPath, "*.txt"))
            {
                Problem problema = new Problem(filePath);
            }
            
        }
    }
}
