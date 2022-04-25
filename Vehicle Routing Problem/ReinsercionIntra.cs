using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public class ReinsercionIntra : EnvironmentStructure
    {
        public Solution LocalSearch(Solution solution, ref int[][] distanceMatrix)
        {
            int size = solution.routes.Count;
            int cost = solution.cost;

            int bestCost = cost;
            int bestRouteIndex = -1;
            int bestOriginIndex = -1;
            int bestDestinationIndex = -1;

            for (int i = 0; i < size; i++)
            {
                List<int> route = solution.routes[i];
                
                for (int j = 1; j < route.Count - 1; j++)
                {
                    for (int k = 1; k < route.Count - 1; k++)
                    {
                        if (k == j) continue;
                        
                        int fromPreviousCost = distanceMatrix[route[j - 1]][route[j]];
                        int toPreviousCost = distanceMatrix[route[j]][route[j + 1]];
                                              
                        int originNextCost = distanceMatrix[route[j - 1]][route[j + 1]];

                        int destinationPreviousCost = distanceMatrix[route[k - 1]][route[k]];

                        int fromNextCost = distanceMatrix[route[k - 1]][route[j]];
                        int toNextCost = distanceMatrix[route[j]][route[k]];

                        if (j < k)
                        {
                            destinationPreviousCost = distanceMatrix[route[k]][route[k + 1]];

                            fromNextCost = distanceMatrix[route[k]][route[j]];
                            toNextCost = distanceMatrix[route[j]][route[k + 1]];
                        }

                        int newCost = cost - fromPreviousCost - toPreviousCost - destinationPreviousCost;
                        newCost += originNextCost + fromNextCost + toNextCost;

                        if (newCost < bestCost)                            
                        {
                            bestRouteIndex = i;
                            bestOriginIndex = j;
                            bestDestinationIndex = k;
                            bestCost = newCost;
                        }
                    }
                }
            }

            if (bestRouteIndex >= 0 && bestOriginIndex >= 0 && bestDestinationIndex >= 0)
            {
                List<int> routeT = solution.routes[bestRouteIndex];
                int temp = routeT[bestOriginIndex];
                routeT.RemoveAt(bestOriginIndex);
                routeT.Insert(bestDestinationIndex, temp);
                solution.cost = bestCost;
            }            

            return solution;
        }
    }
}
