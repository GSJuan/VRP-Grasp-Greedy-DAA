using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public class IntercambioEntre : EnvironmentStructure
    {
        public Solution LocalSearch(Solution solution, ref int[][] distanceMatrix)
        {
            Solution bestSolution = solution;
            int size = solution.routes.Count;
            int cost = solution.cost;

            for (int i = 0; i < size; i++)
            {

                List<int> route = solution.routes[i];
                for (int w = 1; w < size; w++)
                {
                    int otherRouteIndex = i + w;
                    if (otherRouteIndex >= size) { otherRouteIndex = otherRouteIndex % size; }
                    List<int> otherRoute = new List<int>(solution.routes[otherRouteIndex]);

                    for (int j = 1; j < route.Count - 1; j++)
                    {
                        for (int k = 1; k < otherRoute.Count - 1; k++)
                        {
                            List<int> newRoute = new List<int>(route);
                            List<int> newOtherRoute = new List<int>(otherRoute);

                            int fromLeftPreviousCost = distanceMatrix[route[j - 1]][route[j]];
                            int toLeftPreviousCost = distanceMatrix[route[j]][route[j + 1]];

                            int fromRightPreviousCost = distanceMatrix[otherRoute[k - 1]][otherRoute[k]];
                            int toRightPreviousCost = distanceMatrix[otherRoute[k]][otherRoute[k + 1]];

                            (newOtherRoute[k], newRoute[j]) = (newRoute[j], newOtherRoute[k]);

                            int fromLeftNewCost = distanceMatrix[newRoute[j - 1]][newRoute[j]];
                            int toLeftNewCost = distanceMatrix[newRoute[j]][newRoute[j + 1]];

                            int fromRightNewCost = distanceMatrix[newOtherRoute[k - 1]][newOtherRoute[k]];
                            int toRightNewCost = distanceMatrix[newOtherRoute[k]][newOtherRoute[k + 1]];

                            int newCost = cost - fromLeftPreviousCost - toLeftPreviousCost - fromRightPreviousCost - toRightPreviousCost;
                            newCost += fromLeftNewCost + toLeftNewCost + fromRightNewCost + toRightNewCost;

                            if (newCost < bestSolution.cost)
                            {
                                List<List<int>> bestRoutes = new List<List<int>>(solution.routes);
                                bestRoutes[i] = newRoute;
                                bestRoutes[otherRouteIndex] = newOtherRoute;
                                bestSolution = new Solution(bestRoutes, newCost);
                            }
                        }
                    }
                }
            }
            return bestSolution;
        }
    }
}
