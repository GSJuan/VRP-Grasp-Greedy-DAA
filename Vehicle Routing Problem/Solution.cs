using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public class Solution        
    {
        List<List<int>> routes = new List<List<int>>();
        int cost;
        public Solution (List<List<int>> routes, int cost)
        {
            this.routes = routes;
            this.cost = cost;
        }

        public List<List<int>> getRoutes()
        {
            return routes;
        }

        public int getCost()
        {
            return cost;
        }
    }
}
