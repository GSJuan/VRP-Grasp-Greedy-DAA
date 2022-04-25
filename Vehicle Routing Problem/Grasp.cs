using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vehicle_Routing_Problem
{
    internal class Grasp : IAlgorithm
    {
        public Solution Solve(Problem problem)
        {
            //int stop = 0;
            Solution solution;
            int limit = 0;
            int minCost;
            Solution bestSolution;
            EnvironmentStructure structure;
            int[][] distanceMatrix = problem.distanceMatrix;

            structure = new ReinsercionEntre();
            
            //preprocesamiento
            solution = ConstructGreedyRandomizedSolution(problem);
            minCost = solution.getCost();
            bestSolution = solution;
            
            while (limit <= 2000) {

                limit++;
                solution = structure.LocalSearch(solution, ref distanceMatrix);

                if (solution.getCost() < minCost)
                {
                    limit = 0;
                    minCost = solution.getCost();
                    bestSolution = solution;
                }

                solution = ConstructGreedyRandomizedSolution(problem);
                
                
            } 

            return bestSolution;
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
                    while (candidates.Count < 3)
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
