using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day11 : IDay {
        public string SolveP1(string input) {
            string[] directions = input.Split(new string[] { Environment.NewLine, ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> dirCounter = new Dictionary<string, int>();
            dirCounter.Add("nw", 0);
            dirCounter.Add("n", 0);
            dirCounter.Add("ne", 0);
            dirCounter.Add("sw", 0);
            dirCounter.Add("s", 0);
            dirCounter.Add("se", 0);


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

            string mainDir = "";
            string secDir = "";
            if (dirCounter["s"] > dirCounter["n"]) {
                mainDir = "s";
                secDir = "n";
            } else {
                mainDir = "n";
                secDir = "s";
            }
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

            while (dirCounter[mainDir + "w"] > 0 && dirCounter[mainDir + "e"] > 0) {
                dirCounter[mainDir]++;
                dirCounter[mainDir + "w"]--;
                dirCounter[mainDir + "e"]--;
            }
            while (dirCounter[secDir + "w"] > 0 && dirCounter[secDir + "e"] > 0) {
                dirCounter[secDir]++;
                dirCounter[secDir + "w"]--;
                dirCounter[secDir + "e"]--;
            }
            while (dirCounter[mainDir] > 0 && dirCounter[secDir] > 0) {
                dirCounter[mainDir]--;
                dirCounter[secDir]--;
            }

            return $"{String.Join(",", dirCounter.Select(kv => kv.Key + "=" + kv.Value))}  -> Result={dirCounter.Values.Sum()}";
        }

        public string SolveP2(string input) {
            string[] directions = input.Split(new string[] { Environment.NewLine, ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> dirCounter = new Dictionary<string, int>();
            dirCounter.Add("nw", 0);
            dirCounter.Add("n", 0);
            dirCounter.Add("ne", 0);
            dirCounter.Add("sw", 0);
            dirCounter.Add("s", 0);
            dirCounter.Add("se", 0);

            int longestDistance = 0;

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

                int distance = calcDistance(dirCounter);
                if (distance > longestDistance) {
                    longestDistance = distance;
                }
            }

            return $"{longestDistance}";
        }

        private int calcDistance(Dictionary<string, int> dirCounterOrig) {
            Dictionary<string, int> dirCounter = new Dictionary<string, int>(dirCounterOrig);

            string mainDir = "";
            string secDir = "";
            if (dirCounter["s"] > dirCounter["n"]) {
                mainDir = "s";
                secDir = "n";
            } else {
                mainDir = "n";
                secDir = "s";
            }
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

            while (dirCounter[mainDir + "w"] > 0 && dirCounter[mainDir + "e"] > 0) {
                dirCounter[mainDir]++;
                dirCounter[mainDir + "w"]--;
                dirCounter[mainDir + "e"]--;
            }
            while (dirCounter[secDir + "w"] > 0 && dirCounter[secDir + "e"] > 0) {
                dirCounter[secDir]++;
                dirCounter[secDir + "w"]--;
                dirCounter[secDir + "e"]--;
            }
            while (dirCounter[mainDir] > 0 && dirCounter[secDir] > 0) {
                dirCounter[mainDir]--;
                dirCounter[secDir]--;
            }
            return dirCounter.Values.Sum();
        }
    }
}
