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

            return $"{connectedIds.Count}";
        }

        /// <summary>
        /// Create a dictionary which lists for each program to which others it is connected
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private static Dictionary<int, HashSet<int>> GetConnections(string[] lines) {
            Dictionary<int, HashSet<int>> programs = new Dictionary<int, HashSet<int>>();
            foreach (string line in lines) {
                // line format is 'id <-> id2,id3,...'
                string[] values = line.Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

                // id of our program
                int id = Int32.Parse(values[0].Trim());

                // if we do not have it in our dictionary add it with an empty hashset
                if (!programs.ContainsKey(id)) {
                    programs.Add(id, new HashSet<int>());
                }

                // loop through the connected programs
                foreach (String connectedId in values[2].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    int conId = Int32.Parse(connectedId.Trim());
                    // store that this program is connected to our current one
                    if (!programs[id].Contains(conId)) {
                        programs[id].Add(conId);
                    }
                    // create a key for the new program (if we do not have it) and add our current program to its list
                    if (!programs.ContainsKey(conId)) {
                        programs.Add(conId, new HashSet<int>());
                        programs[conId].Add(id);
                    } else if (!programs[conId].Contains(id)) {
                        programs[conId].Add(id);
                    }
                }
            }

            return programs;
        }

        /// <summary>
        /// Adds all ids which are connected to the given id to the Hashset
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hashSet"></param>
        /// <param name="dictionary"></param>
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

            // loop through all the programs and assign them all to groups
            for (int i = 0; i < programs.Keys.Count; i++) {
                if (!IsInAGroup(groups, i)) {
                    HashSet<int> connectedIds = new HashSet<int>();
                    AddConnected(i, connectedIds, programs);
                    groups.Add(i, connectedIds);
                }
            }

            return $"{groups.Keys.Count}";
        }

        /// <summary>
        /// Tests if the given id is in any of the existing groups
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsInAGroup(Dictionary<int, HashSet<int>> groups, int id) {
            return groups.ContainsKey(id) || groups.Any(kv => kv.Value.Contains(id));
        }
    }
}
