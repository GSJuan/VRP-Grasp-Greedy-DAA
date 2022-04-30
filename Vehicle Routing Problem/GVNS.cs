using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vehicle_Routing_Problem
{
    internal class GVNS : IAlgorithm
    {
        public Solution Solve(Problem problem)
        {
            Solution bestLocal;
            Solution local;
            Solution bestSolution;
            Solution microLocal;
            
            int limit = 0;
            int[][] distanceMatrix = problem.distanceMatrix;

            List<EnvironmentStructure> structure = new List<EnvironmentStructure>();            
            structure.Add(new ReinsercionEntre());
            structure.Add(new ReinsercionIntra());
            structure.Add(new IntercambioEntre());
            structure.Add(new IntercambioIntra());
            //structure.Add(new _2OPT());
            
            //preprocesamiento
            bestLocal = ConstructGreedyRandomizedSolution(problem);
            bestSolution = bestLocal;
            
            //multiarranque
            while (limit <= 500) {
                limit++;
                int k = 0;
                //GVNS
                do
                {
                    
                    //SHAKING
                    Solution shaked = Shaking(k, bestLocal, ref distanceMatrix);
                    
                    //VND
                    local = shaked;
                    int l = 0;                   
                    do
                    {
                        microLocal = structure[l].LocalSearch(local, ref distanceMatrix);

                        if (microLocal.GetCost() < local.GetCost())
                        {
                            local = microLocal;
                            l = 0;
                        }
                        else
                        {
                            l++;
                        }
                    } while (l < structure.Count);


                    if (local.GetCost() < bestLocal.GetCost())
                    {
                        bestLocal = local;
                        k = 0;
                    }
                    else
                    {
                        k++;
                    }
                } while (k < structure.Count);
                
                if (bestLocal.GetCost() < bestSolution.GetCost())
                {
                    bestSolution = bestLocal;
                    limit = 0;
                }
                
                //GRASP
                //bestLocal = ConstructGreedyRandomizedSolution(problem);                
            } 

            return bestSolution;
        }

        private Solution Shaking(int k, Solution ogSolution, ref int[][] distanceMatrix)
        {
            int capacity = 24;
            int minimumCapacity = 3;
            Random rnd = new Random();
            List<string> movements = new List<string>();
            Solution solution = new Solution(ogSolution);
            
            for (int i = 0; i <= k; i++)
            {
                int routeIndex;
                int otherRouteIndex;

                do
                {
                    routeIndex = rnd.Next(0, solution.getRoutes().Count);
                    otherRouteIndex = rnd.Next(0, solution.getRoutes().Count);

                } while (otherRouteIndex == routeIndex || solution.getRoutes()[otherRouteIndex].Count >= capacity || solution.getRoutes()[routeIndex].Count <= minimumCapacity);

                List<int> route = solution.getRoutes()[routeIndex];
                List<int> otherRoute = solution.getRoutes()[otherRouteIndex];

                int originIndex;
                int destinationIndex;
                do
                {
                     originIndex = rnd.Next(1, route.Count - 1);
                     destinationIndex = rnd.Next(1, otherRoute.Count - 1);
                } while (movements.Contains(routeIndex.ToString() + otherRouteIndex.ToString() + originIndex.ToString() + destinationIndex.ToString()));


                int temp = route[originIndex];
                
                solution.cost -= distanceMatrix[route[originIndex - 1]][route[originIndex]];
                solution.cost -= distanceMatrix[route[originIndex]][route[originIndex + 1]];
                solution.cost += distanceMatrix[route[originIndex - 1]][route[originIndex + 1]];                
                
                route.RemoveAt(originIndex);
                otherRoute.Insert(destinationIndex, temp);
                
                solution.cost += distanceMatrix[otherRoute[destinationIndex - 1]][otherRoute[destinationIndex]];
                solution.cost += distanceMatrix[otherRoute[destinationIndex]][otherRoute[destinationIndex + 1]];
                solution.cost -= distanceMatrix[otherRoute[destinationIndex - 1]][otherRoute[destinationIndex +1]];

                movements.Add(routeIndex.ToString() + otherRouteIndex.ToString()  + originIndex.ToString() + destinationIndex.ToString());
            }
            return solution;
        }

        private Solution ConstructGreedyRandomizedSolution(Problem problem)
        {
            int[][] distances = problem.distanceMatrix;
            int vehicles = problem.nbVehicles;
            int customers = problem.nbCustomers;

            int capacity = (int)((customers / vehicles) + (customers * 0.1));

            List<List<int>> routes = new List<List<int>>();
            List<int> visited = new List<int>();

            List<int> origin = new List<int>();
            int totalCost = 0;

            Random rnd = new Random();

            for (int i = 0; i < vehicles; i++)
            {
                routes.Add(new List<int>());
                routes[i].Add(0);
                origin.Add(0);
            }

            while (visited.Count < customers)
            {
                for (int i = 0; i < vehicles; i++)
                {
                    if (routes[i].Count > capacity)
                    {
                        continue;
                    } 

                    List<int> candidates = new List<int>();
                    List<int> candidatesCost = new List<int>();

                    //construir lista de candidatos
                    while (candidates.Count < 2)
                    {
                        int next = origin[i];
                        int minDistance = int.MaxValue;
                        for (int j = 1; j <= customers; j++)
                        {
                            int from = origin[i];
                            int distance = distances[from][j];
                            if (!candidates.Contains(j) && distance > 0 && !visited.Contains(j) && distance < minDistance)
                            {
                                minDistance = distance;
                                next = j;
                            }
                        }
                        
                        if(next == origin[i])
                        {
                            break;
                        }
                        
                        candidates.Add(next);
                        candidatesCost.Add(minDistance);
                    }
                    

                    if (candidates.Count == 0)
                    {
                        break;
                    }
                    
                    int index = rnd.Next(candidates.Count);
                    int nextCustomer = candidates[index];
                    visited.Add(nextCustomer);
                    routes[i].Add(nextCustomer);
                    origin[i] = nextCustomer;
                    totalCost += candidatesCost[index];

                }
            }

            for (int i = 0; i < vehicles; i++)
            {
                totalCost += distances[routes[i].Last()][0];
                routes[i].Add(0);
            }

            return new Solution(routes, totalCost);
        }
    }
}
