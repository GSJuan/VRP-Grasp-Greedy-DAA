using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public class IntercambioIntra : EnvironmentStructure
    {
        Solution EnvironmentStructure.LocalSearch(Solution solution, ref int[][] distanceMatrix)
        {
            Solution bestSolution = solution;
            int size = solution.routes.Count;
            int cost = solution.cost;

            for (int i = 0; i < size; i++)
            {
                List<int> route = new List<int>(solution.routes[i]);

                for (int j = 1; j < route.Count - 1; j++)
                {
                    for (int k = j + 1; k < route.Count - 1; k++)
                    {
                        List<int> newRoute = new List<int>(route);

                        int fromLeftPreviousCost = distanceMatrix[route[j - 1]][route[j]];
                        int toLeftPreviousCost = distanceMatrix[route[j]][route[j + 1]];

                        int fromRightPreviousCost = distanceMatrix[route[k - 1]][route[k]];
                        int toRightPreviousCost = distanceMatrix[route[k]][route[k + 1]];

                        (newRoute[j], newRoute[k]) = (newRoute[k], newRoute[j]);


                        int fromLeftNewCost = distanceMatrix[newRoute[j - 1]][newRoute[j]];
                        int toLeftNewCost = distanceMatrix[newRoute[j]][newRoute[j + 1]];
                        
                        int fromRightNewCost = distanceMatrix[newRoute[k - 1]][newRoute[k]];
                        int toRightNewCost = distanceMatrix[newRoute[k]][newRoute[k + 1]];
                        

                        int newCost = cost - fromLeftPreviousCost - toLeftPreviousCost - fromRightPreviousCost - toRightPreviousCost;
                        newCost += fromLeftNewCost + toLeftNewCost + fromRightNewCost + toRightNewCost;

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
