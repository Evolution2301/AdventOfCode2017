using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day15 : IDay {
        static long factorGenA = 16807;
        static long factorGenB = 48271;
        static long divisor = 2147483647;
        static int iterations = 40_000_000;

        public string SolveP1(string input) {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            long valueA = long.Parse(lines[0].Split(' ').Last());
            long valueB = long.Parse(lines[1].Split(' ').Last());

            int matches = 0;
            for (int i = 0; i < iterations; i++) {
                valueA = (valueA * factorGenA) % divisor;
                valueB = (valueB * factorGenB) % divisor;

                if ((valueA & 65535) == (valueB & 65535)) {
                    matches++;
                }
            }

            return $"{matches}";
        }

        public string SolveP2(string input) {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            long[] valuesA = new long[5_000_000];
            long[] valuesB = new long[5_000_000];
            long valueA = long.Parse(lines[0].Split(' ').Last());
            long valueB = long.Parse(lines[1].Split(' ').Last());

            int counterA = 0, counterB = 0;
            while (counterA < 5_000_000 || counterB < 5_000_000) {
                if (counterA < 5_000_000) {
                    valueA = (valueA * factorGenA) % divisor;
                    if (valueA % 4 == 0) {
                        valuesA[counterA++] = valueA;
                    }
                }

                if (counterB < 5_000_000) {
                    valueB = (valueB * factorGenB) % divisor;
                    if (valueB % 8 == 0) {
                        valuesB[counterB++] = valueB;
                    }
                }
            }

            int matches = 0;
            for (int i = 0; i < 5_000_000; i++) {
                if ((valuesA[i] & 65535) == (valuesB[i] & 65535)) {
                    matches++;
                }
            }

            return $"{matches}";
        }
    }
}
