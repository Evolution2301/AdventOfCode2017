using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day6 : IDay {

        public string SolveP1(string input) {
            int cycles = 0;
            // Convert input to integer list
            string[] banksStrings = input.Split(new string[] { Environment.NewLine, " " }, StringSplitOptions.RemoveEmptyEntries);
            List<int> memoryBanks = new List<int>();
            for (int i = 0; i < banksStrings.Length; i++) {
                memoryBanks.Add(int.Parse(banksStrings[i]));
            }

            HashSet<string> knownConfigurations = new HashSet<string>();

            while (!knownConfigurations.Contains(MemoryToString(memoryBanks))) {
                knownConfigurations.Add(MemoryToString(memoryBanks));

                int indexMostBlocks = 0;
                for (int i = 1; i < memoryBanks.Count; i++) {
                    if (memoryBanks[i] > memoryBanks[indexMostBlocks]) {
                        indexMostBlocks = i;
                    }
                }

                int blocks = memoryBanks[indexMostBlocks];
                memoryBanks[indexMostBlocks] = 0;
                for (int i = (indexMostBlocks + 1) % memoryBanks.Count; blocks > 0; i = (i + 1) % memoryBanks.Count, blocks--) {
                    memoryBanks[i]++;
                }
                cycles++;
            }

            return "" + cycles;
        }

        private string MemoryToString(List<int> memoryBanks) {
            string result = "";
            foreach (int i in memoryBanks) {
                result += i + " ";
            }
            return result;
        }

        public string SolveP2(string input) {
            int cycles = 0;
            // Convert input to integer list
            string[] banksStrings = input.Split(new string[] { Environment.NewLine, " " }, StringSplitOptions.RemoveEmptyEntries);
            List<int> memoryBanks = new List<int>();
            for (int i = 0; i < banksStrings.Length; i++) {
                memoryBanks.Add(int.Parse(banksStrings[i]));
            }

            List<string> knownConfigurations = new List<string>();

            while (!knownConfigurations.Contains(MemoryToString(memoryBanks))) {
                knownConfigurations.Add(MemoryToString(memoryBanks));

                int indexMostBlocks = 0;
                for (int i = 1; i < memoryBanks.Count; i++) {
                    if (memoryBanks[i] > memoryBanks[indexMostBlocks]) {
                        indexMostBlocks = i;
                    }
                }

                int blocks = memoryBanks[indexMostBlocks];
                memoryBanks[indexMostBlocks] = 0;
                for (int i = (indexMostBlocks + 1) % memoryBanks.Count; blocks > 0; i = (i + 1) % memoryBanks.Count, blocks--) {
                    memoryBanks[i]++;
                }
            }

            string loopEnd = MemoryToString(memoryBanks);
            for (int i = 0; i < knownConfigurations.Count; i++) {
                if (knownConfigurations[i].Equals(loopEnd)) {
                    cycles = knownConfigurations.Count - i;
                    break;
                }
            }

            return "" + cycles;
        }
    }
}
