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
                if (firewall[packetPos].Check()) {
                    severity += firewall[packetPos].Severity();
                }
                Step(firewall);
            }

            return $"{severity}";
        }

        private List<FirewallLine> CreateFirewall(string[] lines) {
            List<FirewallLine> firewall = new List<FirewallLine>();

            Dictionary<int, int> firewallInfo = new Dictionary<int, int>();
            int biggestKey = 0;
            foreach (string line in lines) {
                string[] splitted = line.Split(':');
                int key = int.Parse(splitted[0].Trim());
                int length = int.Parse(splitted[1].Trim());

                firewallInfo.Add(key, length);
                if (biggestKey < key) {
                    biggestKey = key;
                }
            }

            for (int i = 0; i <= biggestKey; i++) {
                if (!firewallInfo.ContainsKey(i)) {
                    firewall.Add(new FirewallLine(i, 0));
                } else {
                    firewall.Add(new FirewallLine(i, firewallInfo[i]));
                }
            }

            return firewall;
        }

        private void Step(List<FirewallLine> firewall) {
            foreach (FirewallLine firewallLine in firewall) {
                firewallLine.Step();
            }
        }

        private void Reset(List<FirewallLine> firewall) {
            foreach (FirewallLine firewallLine in firewall) {
                firewallLine.Reset();
            }
        }

        public string SolveP2(string input) {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<FirewallLine> firewall = CreateFirewall(lines);

            int delay = -1;
            bool reachedEnd = false;
            do {
                delay++;

                bool caught = false;
                for (int i = 0; i < firewall.Count; i++) {
                    if (firewall[i].Line.Length > 0 && (delay + i) % ((firewall[i].Line.Length - 1) * 2) == 0) {
                        caught = true;
                        break;
                    }
                }
                reachedEnd = !caught;
            } while (!reachedEnd);

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
            Line = new bool[lineLength];
            if (lineLength >= 1) {
                Line[0] = true;
            }
        }

        public void Step() {
            if ((Line?.Length ?? 0) <= 1) {
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
            return (Line?.Length ?? 0) != 0 && Line[0];
        }

        public int Severity() {
            return LineNr * (Line?.Length ?? 0);
        }

        public void Reset() {
            if ((Line?.Length ?? 0) <= 1) {
                return;
            }

            Position = 0;
            Line = new bool[Line.Length];
            if (Line.Length >= 1) {
                Line[0] = true;
            }
        }

        public override string ToString() {
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
