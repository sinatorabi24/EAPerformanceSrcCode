# Brief manual

This manual is associated with the paper **A Method for Performance Analysis 
of a Genetic Algorithm Applied to the Problem of Fuel Consumption Minimization 
for Heavy-Duty Vehicles**, by S. Torabi and M. Wahde.

The code described below carries out the necessary steps for generating 
(one at a time) the five panels in Figure 4 in the paper.

## Usage

To run the code, apply the following steps:

-	In the File menu, select Road and then Load. Open the road file over which 
  the fuel consumption minimization should be carried out. Five roads are provided, 
  namely the 10 km highway road sections between Göteborg and Borås (Roads 1 – 5, 
  located in the Data/Roads/Highway/ folder).
-	Then, optionally, change the GA parameters, by clicking on Set optimization parameters. 
  (The default parameters usually provide good results).
-	Click on Start batch run.

The program will then run a genetic algorithm (GA) the specified number of times (100 per 
default) to generate, in each run, a piecewise linear speed profile (defined by 8 points 
with 9 different levels, per default), for which the fuel consumption is minimal, over 
the road in question. When all 100 runs have been completed (which takes around 11 hours, 
with default settings), the frequency distribution of fuel consumption values (for the best 
speed profile in each run) is automatically saved (in the folder Results/) to a file named 
BatchRunResult_<roadname>.txt, where <roadname> is the name of the road.
In order to compare the frequency distribution obtained from the GA runs to the results 
obtained with a brute force benchmark computation (over all possible piecewise linear speed 
profiles), open Matlab, choose File – Open script, and open the Postprocessing.m script 
(located in the top-level folder). Then press Run (in Matlab).
