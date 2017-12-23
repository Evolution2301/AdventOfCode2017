using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day13 : IDay {
        public string SolveP1(string input) {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<FirewallLine> firewall = CreateFirewall(lines);

            int severity = 0;
            for (int packetPos = 0; packetPos < firewall.Count; packetPos++) {
                // test if we will get caught at our current position
                if (firewall[packetPos].Check()) {
                    // we were caught add the severity to our sum
                    severity += firewall[packetPos].Severity();
                }
                Step(firewall);
            }

            return $"{severity}";
        }

        /// <summary>
        /// take the input and build up the firewall
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<FirewallLine> CreateFirewall(string[] lines) {
            List<FirewallLine> firewall = new List<FirewallLine>();

            int largestLinenumber = 0;
            // first fill a dictionary with the positions and length of the firewall lines
            // store the largest linenumber since that is the end of our firewall
            Dictionary<int, int> firewallInfo = new Dictionary<int, int>();
            foreach (string line in lines) {
                string[] splitted = line.Split(':');
                int key = int.Parse(splitted[0].Trim());
                int length = int.Parse(splitted[1].Trim());

                firewallInfo.Add(key, length);
                if (largestLinenumber < key) {
                    largestLinenumber = key;
                }
            }

            // create the firewall lines (also for the empty lines) <- makes it a bit easier to calculate/loop later on
            for (int i = 0; i <= largestLinenumber; i++) {
                if (!firewallInfo.ContainsKey(i)) {
                    firewall.Add(new FirewallLine(i, 0));
                } else {
                    firewall.Add(new FirewallLine(i, firewallInfo[i]));
                }
            }

            return firewall;
        }

        /// <summary>
        /// Let every scanner perform one step
        /// </summary>
        /// <param name="firewall"></param>
        private void Step(List<FirewallLine> firewall) {
            foreach (FirewallLine firewallLine in firewall) {
                firewallLine.Step();
            }
        }

        public string SolveP2(string input) {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<FirewallLine> firewall = CreateFirewall(lines);

            bool caught = true;
            // Looping through every possibility and testing is way too slow (my solution was at delay ~3.8million)
            // because of this increase delay and calculate whether or not we get caught 
            int delay = 0;
            for (; caught; delay++) {
                caught = false;

                for (int pos = 0; pos < firewall.Count; pos++) {
                    if (// if array for this pos is not populated we do not need to test
                        firewall[pos].Line.Length > 0
                        // delay + pos gives us the time it takes us to get the position
                        // (arraylength for this pos) -1 *2 gives us the time it takes the scanner to get back to cell 0 
                        // -- e.g. with arraylength 3 (starting at 0): time|pos -> 0|0, 1|1, 2|2, 3|1, 4|0 
                        // if (time it takes us to position) modulo (scanner roundtrip time) is 0 we reach the cell at the same time as the scanner
                        // because of this we know we will get caught
                        && (delay + pos) % ((firewall[pos].Line.Length - 1) * 2) == 0) {
                        caught = true;
                        break;
                    }
                }
            }

            return $"{delay}";
        }
    }

    class FirewallLine {
        public int LineNr { get; set; }
        public bool[] Line { get; set; }
        private bool Increasing = true;
        private int Position = 0;

        public FirewallLine(int lineNr, int lineLength) {
            LineNr = lineNr;
            // init array and position scanner at pos 0
            Line = new bool[lineLength];
            if (lineLength >= 1) {
                Line[0] = true;
            }
        }

        public void Step() {
            if ((Line?.Length ?? 0) <= 1) {
                // if length is 1 (or 0) we cannot move
                return;
            }

            Line[Position] = false;
            if (Increasing) {
                if (Position + 1 < Line.Length) {
                    Position++;
                } else {
                    Position--;
                    Increasing = false;
                }
            } else {
                if (Position - 1 >= 0) {
                    Position--;
                } else {
                    Position++;
                    Increasing = true;
                }
            }
            Line[Position] = true;
        }

        public bool Check() {
            // check if length of array is > 0 and whether or not the scanner is at pos 0
            return (Line?.Length ?? 0) != 0 && Line[0];
        }

        public int Severity() {
            // calculate severity for hitting the scanner at this level
            return LineNr * (Line?.Length ?? 0);
        }

        public override string ToString() {
            // For debugging purposes
            if ((Line?.Length ?? 0) > 0) {
                String s = "";
                for (int i = 0; i < Line.Length; i++) {
                    s += $"[{(Line[i] ? "S" : " ")}]";
                }
                return s;
            } else {
                return "...";
            }
        }
    }
}
