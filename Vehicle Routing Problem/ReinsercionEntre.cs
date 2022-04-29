using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public class ReinsercionEntre : EnvironmentStructure
    {
        public Solution LocalSearch(Solution solution, ref int[][] distanceMatrix)
        {
            int size = solution.routes.Count;
            int cost = solution.cost;

            int bestCost = cost;
            int bestRouteIndex = -1;
            int bestOtherRouteIndex = -1;
            int bestOriginIndex = -1;
            int bestDestinationIndex = -1;

            for (int i = 0; i < size; i++)
            {
                List<int> route = solution.routes[i];
                
                for (int w = 1; w < size; w++)
                {
                    int otherRouteIndex = i + w;
                    if (otherRouteIndex >= size) { otherRouteIndex = otherRouteIndex % size; }
                    List<int> otherRoute = solution.routes[otherRouteIndex];

                    for (int j = 1; j < route.Count - 1; j++)
                    {
                        int fromPreviousCost = distanceMatrix[route[j - 1]][route[j]];
                        int toPreviousCost = distanceMatrix[route[j]][route[j + 1]];

                        int originNextCost = distanceMatrix[route[j - 1]][route[j + 1]];

                        for (int k = 1; k < otherRoute.Count - 1; k++)
                        {                           

                            int destinationPreviousCost = distanceMatrix[otherRoute[k - 1]][otherRoute[k]];

                            int fromNextCost = distanceMatrix[otherRoute[k - 1]][route[j]];
                            int toNextCost = distanceMatrix[route[j]][otherRoute[k]];

                            

                            int newCost = cost - fromPreviousCost - toPreviousCost - destinationPreviousCost;
                            newCost += originNextCost + fromNextCost + toNextCost;

                            if (newCost < bestCost)
                            {
                                bestRouteIndex = i;
                                bestOriginIndex = j;
                                bestDestinationIndex = k;
                                bestOtherRouteIndex = otherRouteIndex;
                                bestCost = newCost;
                            }
                        }
                    }
                }
            }
            
            if (bestRouteIndex >= 0 && bestOriginIndex >= 0 && bestDestinationIndex >= 0)
            {
                Solution bestFromLocal = new Solution(solution);
                List<int> routeT = bestFromLocal.routes[bestRouteIndex];
                int temp = routeT[bestOriginIndex];
                routeT.RemoveAt(bestOriginIndex);
                bestFromLocal.routes[bestOtherRouteIndex].Insert(bestDestinationIndex, temp);
                bestFromLocal.cost = bestCost;
                return bestFromLocal;
            }
            
            return solution;
        }
    }
}
