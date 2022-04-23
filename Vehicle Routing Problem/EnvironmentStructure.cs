using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public interface EnvironmentStructure
    {
        Solution LocalSearch(Solution solution, ref int[][] distanceMatrix);
    }
}
