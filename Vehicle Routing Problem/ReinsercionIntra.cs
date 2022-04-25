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
            int size = solution.routes.Count;
            int cost = solution.cost;

            for (int i = 0; i < size; i++)
            {
                List<int> route = new List<int>(solution.routes[i]);
                
                for (int j = 1; j < route.Count - 1; j++)
                {
                    for (int k = 1; k < route.Count - 1; k++)
                    {
                        if (k == j) continue;

                        List<int> newRoute = new List<int>(route);
                        
                        int fromPreviousCost = distanceMatrix[route[j - 1]][route[j]];
                        int toPreviousCost = distanceMatrix[route[j]][route[j + 1]];
                        
                        newRoute.RemoveAt(j);
                        newRoute.Insert(k, route[j]);

                        int newCost = cost - fromPreviousCost - toPreviousCost + distanceMatrix[route[j - 1]][route[j + 1]] - distanceMatrix[route[k - 1]][route[k]];
                        newCost += distanceMatrix[newRoute[k - 1]][newRoute[k]] + distanceMatrix[newRoute[k]][newRoute[k + 1]];
                        
                        if (newCost < bestSolution.cost)
                        {
                            List<List<int>> bestRoutes = new List<List<int>>(solution.routes);
                            bestRoutes[i] = newRoute;
                            bestSolution = new Solution(bestRoutes, newCost);
                        }
                    }
                }
            }
            return bestSolution;
        }
    }
}
