using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle_Routing_Problem
{
    public interface IAlgorithm
    {
        public Solution Solve(Problem problem);
    }
}
