using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day2 : IDay {
        public string SolveP1(string input) {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;
            foreach (string line in lines) {
                int smallest = -1, largest = -1;
                string[] splitted = line.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string num in splitted) {
                    int c = int.Parse(num);
                    if (smallest < 0) {
                        smallest = c;
                    } else if (smallest > c) {
                        if (smallest > largest) {
                            largest = smallest;
                        }
                        smallest = c;
                    } else if (c > largest) {
                        largest = c;
                    }
                }
                sum += largest - smallest;
            }
            return $"{sum}";
        }

        public string SolveP2(string input) {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;
            foreach (string line in lines) {
                string[] splitted = line.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < splitted.Length;i++) {
                    int num = int.Parse(splitted[i]);
                    for(int j = 0; j < splitted.Length;j++) {
                        if (j != i) {
                            int num2 = int.Parse(splitted[j]);
                            if (num2 % num == 0) {
                                sum += num2 / num;
                                goto multibreak;
                            }
                        }
                    }
                }
                multibreak: continue;
            }
            return $"{sum}";
        }
    }
}
