using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day8 : IDay {
        public string SolveP1(string input) {
            Dictionary<string, int> register = new Dictionary<string, int>();
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines) {
                string[] instructions = line.Split(' ');
                string regKey = instructions[0];
                if (!register.ContainsKey(regKey)) {
                    register.Add(regKey, 0);
                }

                bool performAction = false;

                string checkKey = instructions[4];
                if (!register.ContainsKey(checkKey)) {
                    register.Add(checkKey, 0);
                }

                int checkVal = int.Parse(instructions[6]);

                switch (instructions[5]) {
                    case ">":
                        performAction = register[checkKey] > checkVal;
                        break;
                    case "<":
                        performAction = register[checkKey] < checkVal;
                        break;
                    case ">=":
                        performAction = register[checkKey] >= checkVal;
                        break;
                    case "==":
                        performAction = register[checkKey] == checkVal;
                        break;
                    case "<=":
                        performAction = register[checkKey] <= checkVal;
                        break;
                    case "!=":
                        performAction = register[checkKey] != checkVal;
                        break;
                }

                if (performAction) {
                    if (instructions[1].Equals("inc")) {
                        register[regKey] += int.Parse(instructions[2]);
                    } else {
                        register[regKey] -= int.Parse(instructions[2]);
                    }
                }
            }

            int largestValue = -1;
            foreach (string key in register.Keys) {
                if (register[key] > largestValue) {
                    largestValue = register[key];
                }
            }
            return "" + largestValue;
        }

        public string SolveP2(string input) {
            Dictionary<string, int> register = new Dictionary<string, int>();
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int highesValueEver = -1;

            foreach (string line in lines) {
                string[] instructions = line.Split(' ');
                string regKey = instructions[0];
                if (!register.ContainsKey(regKey)) {
                    register.Add(regKey, 0);
                }

                bool performAction = false;

                string checkKey = instructions[4];
                if (!register.ContainsKey(checkKey)) {
                    register.Add(checkKey, 0);
                }

                int checkVal = int.Parse(instructions[6]);

                switch (instructions[5]) {
                    case ">":
                        performAction = register[checkKey] > checkVal;
                        break;
                    case "<":
                        performAction = register[checkKey] < checkVal;
                        break;
                    case ">=":
                        performAction = register[checkKey] >= checkVal;
                        break;
                    case "==":
                        performAction = register[checkKey] == checkVal;
                        break;
                    case "<=":
                        performAction = register[checkKey] <= checkVal;
                        break;
                    case "!=":
                        performAction = register[checkKey] != checkVal;
                        break;
                }

                if (performAction) {
                    if (instructions[1].Equals("inc")) {
                        register[regKey] += int.Parse(instructions[2]);
                    } else {
                        register[regKey] -= int.Parse(instructions[2]);
                    }
                }

                if (highesValueEver < register[regKey]) {
                    highesValueEver = register[regKey];
                }
            }

            return "" + highesValueEver;
        }
    }
}
