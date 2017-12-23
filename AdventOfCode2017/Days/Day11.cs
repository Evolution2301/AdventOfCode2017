using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day11 : IDay {
        public string SolveP1(string input) {
            string[] directions = input.Split(new string[] { Environment.NewLine, ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> dirCounter = CreateDirectionDictionary(directions).Item1;

            return $"{CalcDistance(dirCounter)}";
        }

        private (Dictionary<string, int>, int) CreateDirectionDictionary(string[] directions, bool calcLongestDistance = false) {
            // Init dictionary with all directions
            Dictionary<string, int> dirCounter = new Dictionary<string, int> {
                { "nw", 0 },
                { "n", 0 },
                { "ne", 0 },
                { "sw", 0 },
                { "s", 0 },
                { "se", 0 }
            };

            int longestDistance = 0;

            // we can reduce the directions during creation since 'nw' and 'se', 'n' and 's', 'ne' and 'sw' delete each other
            foreach (string direction in directions) {
                switch (direction) {
                    case "nw":
                        if (dirCounter["se"] > 0) {
                            dirCounter["se"]--;
                        } else {
                            dirCounter["nw"]++;
                        }
                        break;
                    case "n":
                        if (dirCounter["s"] > 0) {
                            dirCounter["s"]--;
                        } else {
                            dirCounter["n"]++;
                        }
                        break;
                    case "ne":
                        if (dirCounter["sw"] > 0) {
                            dirCounter["sw"]--;
                        } else {
                            dirCounter["ne"]++;
                        }
                        break;
                    case "sw":
                        if (dirCounter["ne"] > 0) {
                            dirCounter["ne"]--;
                        } else {
                            dirCounter["sw"]++;
                        }
                        break;
                    case "s":
                        if (dirCounter["n"] > 0) {
                            dirCounter["n"]--;
                        } else {
                            dirCounter["s"]++;
                        }
                        break;
                    case "se":
                        if (dirCounter["nw"] > 0) {
                            dirCounter["nw"]--;
                        } else {
                            dirCounter["se"]++;
                        }
                        break;
                }
            }

            if (calcLongestDistance) {
                int currentDistance = CalcDistance(dirCounter);
                if (currentDistance > longestDistance) {
                    longestDistance = currentDistance;
                }
            }

            return (dirCounter, longestDistance);
        }

        public string SolveP2(string input) {
            string[] directions = input.Split(new string[] { Environment.NewLine, ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            return $"{CreateDirectionDictionary(directions, true).Item2}";
        }

        /// <summary>
        /// Calculates the distance to the center by reducing the directions as far as possible returning the sum
        /// </summary>
        /// <param name="dirCounterOrig"></param>
        /// <returns></returns>
        private int CalcDistance(Dictionary<string, int> dirCounterOrig) {
            Dictionary<string, int> dirCounter = new Dictionary<string, int>(dirCounterOrig); // create a copy of the dictionary, otherwise we would change it

            // determine which direction has more entries (one of them is 0)
            string mainDir = "";
            string secDir = "";
            if (dirCounter["s"] > dirCounter["n"]) {
                mainDir = "s";
                secDir = "n";
            } else {
                mainDir = "n";
                secDir = "s";
            }

            // if we have the entries 's' and 'nw' we can reduce this to one 'sw' entry
            while (dirCounter[mainDir] > 0 && (dirCounter[secDir + "w"] > 0 || dirCounter[secDir + "e"] > 0)) {
                if (dirCounter[secDir + "w"] > 0) {
                    dirCounter[mainDir + "w"]++;
                    dirCounter[secDir + "w"]--;
                    dirCounter[mainDir]--;
                } else if (dirCounter[secDir + "e"] > 0) {
                    dirCounter[mainDir + "e"]++;
                    dirCounter[secDir + "e"]--;
                    dirCounter[mainDir]--;
                }
            }

            // 'nw' and 'ne' result in one 'n' entry
            while (dirCounter[mainDir + "w"] > 0 && dirCounter[mainDir + "e"] > 0) {
                dirCounter[mainDir]++;
                dirCounter[mainDir + "w"]--;
                dirCounter[mainDir + "e"]--;
            }
            // the same with the other direction
            while (dirCounter[secDir + "w"] > 0 && dirCounter[secDir + "e"] > 0) {
                dirCounter[secDir]++;
                dirCounter[secDir + "w"]--;
                dirCounter[secDir + "e"]--;
            }
            // in the previous loops we created 'n' and 's' entries, those delete each other again
            while (dirCounter[mainDir] > 0 && dirCounter[secDir] > 0) {
                dirCounter[mainDir]--;
                dirCounter[secDir]--;
            }

            // all remaining values add up to the final distance
            return dirCounter.Values.Sum();
        }
    }
}
