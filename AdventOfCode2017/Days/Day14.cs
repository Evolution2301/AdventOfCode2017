using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day14 : IDay {

        public string SolveP1(string input) {
            string[] mesh = CreateMesh(input);

            int used = mesh.Sum(ml => ml.Sum(mc => mc - '0'));
            return $"{used}";
        }

        private string[] CreateMesh(string input) {
            string[] mesh = new string[128];
            for (int i = 0; i < 128; i++) {
                Day10 knotHashDay = new Day10();
                string knotHash = knotHashDay.SolveP2($"{input}-{i}");
                mesh[i] = GetBitRepresentation(knotHash);
            }
            return mesh;
        }

        private string GetBitRepresentation(string knotHash) {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < knotHash.Length; i++) {
                int value = 0;
                if (knotHash[i] < 'a') {
                    value = knotHash[i] - '0';
                } else {
                    value = 10 + knotHash[i] - 'a';
                }
                stringBuilder.Append(Convert.ToString(value, 2).PadLeft(4, '0'));
            }
            return stringBuilder.ToString();
        }

        public string SolveP2(string input) {
            string[] mesh = CreateMesh(input);
            int maxGroup = 0;

            // record for every position where there is a one to which group it belongs
            Dictionary<string, int> posAndGroup = new Dictionary<string, int>();
            for (int x = 0; x < mesh.Length; x++) {
                for (int y = 0; y < mesh[x].Length; y++) {
                    if (mesh[x][y] == '1') {
                        HashSet<int> neighborGroups = CheckForGroups(x, y, posAndGroup);
                        if (neighborGroups.Count == 0) {
                            posAndGroup.Add($"{x},{y}", ++maxGroup);
                        } else if (neighborGroups.Count == 1) {
                            posAndGroup.Add($"{x},{y}", neighborGroups.First());
                        } else {
                            // more than one neighboring group -> merge all of those
                            int firstGroup = neighborGroups.Min();
                            foreach (int group in neighborGroups.Where(g => g != firstGroup)) {
                                foreach (string key in posAndGroup.Where(pag => pag.Value == group).Select(kvp => kvp.Key).ToList()) {
                                    posAndGroup[key] = firstGroup;
                                }
                            }
                            posAndGroup.Add($"{x},{y}", firstGroup);
                        }
                    }
                }
            }
            //GroupsToString(mesh, posAndGroup);

            return $"{posAndGroup.Values.Distinct().Count()}";
        }

        /// <summary>
        /// For debugging
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="groups"></param>
        private void GroupsToString(string[] mesh, Dictionary<string, int> groups) {
            StringBuilder groupsMesh = new StringBuilder();
            int padLength = groups.Values.Max().ToString().Length;

            for (int x = 0; x < mesh.Length; x++) {
                for (int y = 0; y < mesh[x].Length; y++) {
                    if (InGroup(x, y, groups, out int groupNr)) {
                        groupsMesh.Append($"[{groupNr.ToString().PadLeft(padLength, ' ')}]");
                    } else {
                        groupsMesh.Append($"[{(" ".PadLeft(padLength, ' '))}]");
                    }
                }
                groupsMesh.AppendLine(";");
            }
            Console.WriteLine(groupsMesh);
            return;
        }

        private HashSet<int> CheckForGroups(int x, int y, Dictionary<string, int> groups) {
            HashSet<int> groupNrs = new HashSet<int>();
            // We only need to check the values 'above' and to the left, since we iterate from top left to bottom right
            if (y > 0) {
                CheckFieldForGroup(x, y - 1, groups, groupNrs);
            }
            if (x > 0) {
                CheckFieldForGroup(x - 1, y, groups, groupNrs);
            }
            return groupNrs;
        }

        private void CheckFieldForGroup(int x, int y, Dictionary<string, int> groups, HashSet<int> groupNrs) {
            if (InGroup(x, y, groups, out int groupNr)) {
                groupNrs.Add(groupNr);
            }
        }

        private bool InGroup(int x, int y, Dictionary<string, int> groups, out int groupNr) {
            groupNr = 0;
            String key = $"{x},{y}";
            if (groups.ContainsKey(key)) {
                groupNr = groups[key];
                return true;
            }
            return false;
        }
    }
}
