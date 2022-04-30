using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    //Interfaz que representa estructuras de entorno
    public interface EnvironmentStructure
    {
        Solution LocalSearch(Solution solution, ref int[][] distanceMatrix);
    }
}
