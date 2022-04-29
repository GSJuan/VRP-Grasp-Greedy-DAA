using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public class Solution        
    {
        public List<List<int>> routes = new List<List<int>>();
        public int cost;
        public Solution (List<List<int>> routes, int cost)
        {
            this.routes = routes;
            this.cost = cost;
        }

        public Solution (Solution solution)
        {
            routes = new List<List<int>>();
            for (int i = 0; i < solution.routes.Count; i++) routes.Add(new List<int>(solution.routes[i]));
            cost = solution.cost;
        }

        public List<List<int>> getRoutes()
        {
            return routes;
        }

        public int getCost()
        {
            return cost;
        }
        
        public int calculateCost(ref int[][] distanceMatrix)
        {
            int cost = 0;
            foreach (List<int> route in routes)
            {
                for (int i = 0; i < route.Count - 1; i++)
                {
                    cost += distanceMatrix[route[i]][route[i + 1]];
                }
            }
            return cost;
        }
    }
}
