using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public class IntercambioEntre : EnvironmentStructure
    {
        public Solution LocalSearch(Solution solution, ref int[][] distanceMatrix)
        {
            int size = solution.routes.Count;
            int cost = solution.cost;

            int bestCost = cost;
            int bestRouteIndex = -1;
            int bestOriginIndex = -1;
            int bestOtherRouteIndex = -1;
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
                        for (int k = j + 1; k < otherRoute.Count - 1; k++)
                        {
                            int fromLeftPreviousCost = distanceMatrix[route[j - 1]][route[j]];
                            int toLeftPreviousCost = distanceMatrix[route[j]][route[j + 1]];

                            int fromRightPreviousCost = distanceMatrix[otherRoute[k - 1]][otherRoute[k]];
                            int toRightPreviousCost = distanceMatrix[otherRoute[k]][otherRoute[k + 1]];


                            int fromLeftNewCost = distanceMatrix[route[j - 1]][otherRoute[k]];
                            int toLeftNewCost = distanceMatrix[otherRoute[k]][route[j + 1]];

                            int fromRightNewCost = distanceMatrix[otherRoute[k - 1]][route[j]];
                            int toRightNewCost = distanceMatrix[route[j]][otherRoute[k + 1]];

                            int newCost = cost - fromLeftPreviousCost - toLeftPreviousCost - fromRightPreviousCost - toRightPreviousCost;
                            newCost += fromLeftNewCost + toLeftNewCost + fromRightNewCost + toRightNewCost;

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
                List<int> routeT = solution.routes[bestRouteIndex];
                List<int> otherRouteT = solution.routes[bestOtherRouteIndex];

                (otherRouteT[bestDestinationIndex], routeT[bestOriginIndex]) = (routeT[bestOriginIndex], otherRouteT[bestDestinationIndex]);
                solution.cost = bestCost;
            }
            
            return solution;
        }
    }
}
