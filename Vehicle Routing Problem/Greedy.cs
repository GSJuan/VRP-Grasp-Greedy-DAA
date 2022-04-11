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
            int[][] distances = problem.distanceMatrix;
            int vehicles = problem.nbVehicles;
            int customers = problem.nbCustomers;

            List<List<int>> routes = new List<List<int>>();
            List<int> visited = new List<int>();

            List<int> origin = new List<int>();
            int totalCost = 0;

            for (int i = 0; i < vehicles; i++)
            {
                routes.Add(new List<int>());
                routes[i].Add(0);
                origin.Add(0);
            }

            while(visited.Count < customers )
            {
                for (int i = 0; i < vehicles; i++)
                {                                                            
                    int minDistance = int.MaxValue;
                    int next = origin[i];
                    
                    for (int j = 1; j <= customers; j++)
                    {
                        int from = origin[i];
                        int distance = distances[from][j];
                        if (distance != 0 && !visited.Contains(j) && distance < minDistance)
                        {
                            minDistance = distance;
                            next = j;
                        }
                    }

                    if (next == origin[i])
                    {
                        break;
                    }
                    
                    visited.Add(next);
                    routes[i].Add(next);                    
                    origin[i] = next;
                    totalCost += minDistance;                                             

                }
            }

            for (int i = 0; i < vehicles; i++)
            {
                totalCost += distances[routes[i].Last()][0];
                routes[i].Add(0);
                
            }

            return new Solution(routes, totalCost);
        }
    }
}
