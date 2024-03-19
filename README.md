# Vehicle Routing Problem (VRP) Solver

## Introduction
This project presents a coded solution in C# for the Vehicle Routing Problem (VRP) as part of Practice 7 for the Design and Analysis of Algorithms course at the University of La Laguna, 2021-2022. It explains the three implemented algorithms and the data structures used.

## Project Structure
- `Problem Class`: Represents the VRP, storing information like the number of available vehicles and clients, and a cost matrix for traveling from client i to client j.
- `Solution Class`: Represents a VRP solution, containing the total cost and a list of lists of integers for routes.
- `IAlgorithm`: Interface implemented by the three algorithms (GRASP, Greedy, and GVNS). Defines the `Solve` method which executes each algorithm and returns a solution.
- `EnvironmentStructure`: Interface defining the `LocalSearch` method implemented by all environment structures (2OPT, Reinsertion, and Exchange within and between routes).

## Algorithms
1. **Greedy**: Selects the best possible client at each step, constructing routes for each vehicle in parallel.
2. **GRASP**: 
   - *Initialization & Construction*: Generates an initial solution using `ConstructGreedyRandomizedSolution`.
   - *Improvement*: Applies improvements using the specified environment structure.
   - *Update*: Updates the global best solution if a better solution is found.
3. **GVNS**: 
   - *Initialization*: Uses GRASP for the initial solution and defines a list of environment structures.
   - *Shaking*: Applies diversification based on the initial solution.
   - *Intensification*: Uses VND (Variable Neighborhood Descent) for local search.
   - *Update*: Updates the global best solution if a better solution is found.

## Results
### Greedy Algorithm
| Number of Vehicles | Total Cost | Time (ms) |
|--------------------|------------|-----------|
| 2                  | 227        | 1         |
| 4                  | 281        | 0         |
| 6                  | 352        | 0         |
| 8                  | 484        | 0         |

### GRASP Algorithm
| Number of Vehicles | Environment Structure | Total Cost | Time (ms) |
|--------------------|-----------------------|------------|-----------|
| 2                  | Reinsertion Between   | 133        | 283       |
| 2                  | Reinsertion Within    | 137        | 433       |
| 2                  | Exchange Between      | 151        | 240       |
| 2                  | Exchange Within       | 152        | 242       |
| 4                  | Reinsertion Between   | 154        | 442       |
| ...                | ...                   | ...        | ...       |

### GVNS Algorithm
| Number of Vehicles | K max value | Total Cost | Time (ms) |
|--------------------|-------------|------------|-----------|
| 2                  | 4           | 114        | 496       |
| 4                  | 4           | 137        | 347       |
| 6                  | 4           | 171        | 478       |
| 8                  | 4           | 203        | 456       |
| 2                  | 3           | 124        | 290       |
| 4                  | 3           | 143        | 258       |
| 6                  | 3           | 182        | 256       |
| 8                  | 3           | 221        | 298       |

## Running the Project
To run this project, a C# environment setup is required. After cloning the repository, compile and run the project using an IDE or command line. Select an algorithm to solve the VRP and review the results as detailed in the project instructions.

## Contributors
- Juan García Santos (alu0101325583@ull.edu.es)

*University of La Laguna, Design and Analysis of Algorithms Course, 2021-2022*
