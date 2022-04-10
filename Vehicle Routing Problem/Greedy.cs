using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Vehicle_Routing_Problem
{
    internal class Greedy : IAlgorithm
    {
        public Solution Solve(Problem problem)
        {
            List<List<int>> routes = new List<List<int>>();
            List<int> visited = new List<int>();
            int[][] distances = problem.distanceMatrix;
            int vehicles = problem.nbVehicles;
            int customers = problem.nbCustomers;

            int totalCost = 0;

            for (int i = 0; i < vehicles; i++)
            {
                routes.Add(new List<int>());
                routes[i].Add(0);
                int origin = 0;
                do
                {
                    int minDistance = int.MaxValue;
                    int next = 0;
                    for (int j = 0; j <= customers; j++)
                    {
                        int distance = distances[origin][j];
                        if (distance != 0 && !visited.Contains(j) && distance < minDistance)
                        {
                            minDistance = distance;
                            next = j;
                        }
                    }
                    origin = next;

                    if (origin != 0 || i == vehicles - 1)
                    {
                        visited.Add(origin);
                    }
                    
                    routes[i].Add(origin);
                    
                    if (minDistance != int.MaxValue)
                    {
                        totalCost += minDistance;
                    }                

                } while (origin != 0);
                
            }

            return new Solution(routes, totalCost);
        }
    }
}
