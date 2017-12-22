using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day12 : IDay {
        public string SolveP1(string input) {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<int, HashSet<int>> programs = GetConnections(lines);

            HashSet<int> connectedIds = new HashSet<int>();
            AddConnected(0, connectedIds, programs);

            return connectedIds.Count + "";
        }

        private static Dictionary<int, HashSet<int>> GetConnections(string[] lines) {
            Dictionary<int, HashSet<int>> programs = new Dictionary<int, HashSet<int>>();
            foreach (string line in lines) {
                string[] values = line.Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

                int id = Int32.Parse(values[0].Trim());

                if (!programs.ContainsKey(id)) {
                    programs.Add(id, new HashSet<int>());
                }

                foreach (String connectedId in values[2].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    int conId = Int32.Parse(connectedId.Trim());
                    if (!programs[id].Contains(conId)) {
                        programs[id].Add(conId);
                    }
                    if (!programs.ContainsKey(conId)) {
                        programs.Add(conId, new HashSet<int>());
                    }
                    if (!programs[conId].Contains(id)) {
                        programs[conId].Add(id);
                    }
                }
            }

            return programs;
        }

        private void AddConnected(int id, HashSet<int> hashSet, Dictionary<int, HashSet<int>> dictionary) {
            if (!hashSet.Contains(id)) {
                hashSet.Add(id);
                foreach (int conId in dictionary[id]) {
                    AddConnected(conId, hashSet, dictionary);
                }
            }
        }

        public string SolveP2(string input) {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<int, HashSet<int>> programs = GetConnections(lines);
            Dictionary<int, HashSet<int>> groups = new Dictionary<int, HashSet<int>>();

            for (int i = 0; i < programs.Keys.Count; i++) {
                if (!IsInGroups(groups, i)) {
                    HashSet<int> connectedIds = new HashSet<int>();
                    AddConnected(i, connectedIds, programs);
                    groups.Add(i, connectedIds);
                }
            }

            return $"{groups.Keys.Count}";
        }

        private bool IsInGroups(Dictionary<int, HashSet<int>> groups, int id) {
            if (groups.ContainsKey(id)) {
                return true;
            }
            return groups.Any(kv => kv.Value.Contains(id));
        }
    }
}
