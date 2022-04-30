using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    //Estructura de entorno basada en el algoritmo de búsqueda local 2OPT
    public class _2OPT : EnvironmentStructure
    {
        public Solution LocalSearch(Solution solution, ref int[][] distanceMatrix)
        {            
            return solution;
        }
        
        public List<int> OPTSwap (ref List<int> route, int i , int k)
        {
            List<int> newRoute = new List<int>();

            for (int j = 0; j < i; j++)
            {
                newRoute.Add(route[j]);
            }

            for (int j = k; j >= i; j--)
            {
                newRoute.Add(route[j]);
            }

            for (int j = k + 1; j < route.Count; j++)
            {
                newRoute.Add(route[j]);
            }
            
            return newRoute;
        }
    }
}
