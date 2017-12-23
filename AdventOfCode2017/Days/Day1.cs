using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day1 : IDay {
        public string SolveP1(string input) {
            return $"{CalculateSum(input, 1)}";
        }

        public string SolveP2(string input) {
            int step = input.Length / 2;
            return $"{CalculateSum(input, step)}";
        }

        private static int CalculateSum(string input, int step) {
            int sum = 0;
            for (int i = 0; i < input.Length; i++) {
                // to avoid running checking outside of the array, we are using modulo
                if (input[i] == input[(i + step) % input.Length]) {
                    // if the curent position matches the checked position we can the value to our sum
                    sum += int.Parse("" + input[i]);
                }
            }

            return sum;
        }
    }
}
