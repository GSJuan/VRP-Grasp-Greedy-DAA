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

            int bestCost = cost;
            int bestRouteIndex = -1;
            int bestOriginIndex = -1;
            int bestDestinationIndex = -1;

            for (int i = 0; i < size; i++)
            {
                List<int> route = solution.routes[i];

                for (int j = 1; j < route.Count - 1; j++)
                {
                    for (int k = j + 1; k < route.Count - 1; k++)
                    {                     
                       
                        int fromLeftPreviousCost = distanceMatrix[route[j - 1]][route[j]];
                        int toLeftPreviousCost = distanceMatrix[route[j]][route[j + 1]];

                        int fromRightPreviousCost = distanceMatrix[route[k - 1]][route[k]];
                        int toRightPreviousCost = distanceMatrix[route[k]][route[k + 1]];                        


                        int fromLeftNewCost = distanceMatrix[route[j - 1]][route[k]];
                        int toLeftNewCost = distanceMatrix[route[k]][route[j + 1]];
                        
                        int fromRightNewCost = distanceMatrix[route[k - 1]][route[j]];
                        int toRightNewCost = distanceMatrix[route[j]][route[k + 1]];
                       
                        if(k - 1 == j)
                        {
                            fromRightPreviousCost = 0;
                            
                            toLeftNewCost = 0;
                            fromRightNewCost = distanceMatrix[route[k]][route[j]];
                        }
                       
                        int newCost = cost - fromLeftPreviousCost - toLeftPreviousCost - fromRightPreviousCost - toRightPreviousCost;
                        newCost += fromLeftNewCost + toLeftNewCost + fromRightNewCost + toRightNewCost;

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
                (routeT[bestOriginIndex], routeT[bestDestinationIndex]) = (routeT[bestDestinationIndex], routeT[bestOriginIndex]);                
                solution.cost = bestCost;
            }

            return solution;
        }
    }
}
