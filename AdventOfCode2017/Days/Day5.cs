using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day5 : IDay {
        public string SolveP1(string input) {
            int steps = 0;

            // Convert input to integer list
            string[] jumpsStrings = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<int> jumps = new List<int>();
            for (int i = 0; i < jumpsStrings.Length; i++) {
                jumps.Add(int.Parse(jumpsStrings[i]));
            }

            // Iterate through list, follow the indexes and increase the steps, until the next index is out of the list
            for (int index = 0; index < jumps.Count; steps++) {
                int newIndex = index + jumps[index];
                jumps[index] = jumps[index] + 1;
                index = newIndex;
            }

            return "" + steps;
        }

        public string SolveP2(string input) {
            int steps = 0;

            // Convert input to integer list
            string[] jumpsStrings = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<int> jumps = new List<int>();
            for (int i = 0; i < jumpsStrings.Length; i++) {
                jumps.Add(int.Parse(jumpsStrings[i]));
            }

            // Iterate through list, follow the indexes and increase the steps, until the next index is out of the list
            for (int index = 0; index < jumps.Count; steps++) {
                int newIndex = index + jumps[index];
                // offset >= 3? 
                if (Math.Abs(newIndex) - Math.Abs(index) >= 3) {
                    jumps[index] = jumps[index] - 1;
                } else {
                    jumps[index] = jumps[index] + 1;
                }
                index = newIndex;
            }

            return "" + steps;
        }
    }
}
