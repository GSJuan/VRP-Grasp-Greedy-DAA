using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public class ReinsercionIntra : EnvironmentStructure
    {
        public Solution LocalSearch(Solution solution, ref int[][] distanceMatrix)
        {
            Solution bestSolution = solution;
            List<List<int>> routes = solution.routes;
            int cost = solution.cost;

            for (int k = 0; k < routes.Count; k++)
            {
                List<int> route = routes[k];
                for (int i = 1; i < route.Count - 1; i++)
                {
                    for (int j = i + 1; j < route.Count - 1; j++)
                    {
                        List<int> newRoute = route;
                        int previousCost = distanceMatrix[route[i - 1]][route[i]];
                        int nextCost = distanceMatrix[route[i]][route[i + 1]];
                        newRoute.RemoveAt(i);
                        newRoute.Insert(j, route[i]);
                        int newCost = cost - previousCost - nextCost;
                        newCost += distanceMatrix[newRoute[j - 1]][newRoute[j]] + distanceMatrix[newRoute[j]][newRoute[j + 1]];
                        
                        if (cost < bestSolution.cost)
                        {
                            List<List<int>> bestRoutes = routes;
                            bestRoutes[k] = newRoute;
                            bestSolution = new Solution(bestRoutes, newCost);
                        }
                    }
                }
            }
            return bestSolution;
        }
    }
}
