using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day1 : IDay {
        public string SolveP1(string input) {
            int sum = 0;
            for(int i=0; i < input.Length; i++) {
                if(i+1 == input.Length) {
                    if(input[i] == input[0]) {
                        sum += int.Parse(""+input[i]);
                    }
                } else {
                    if(input[i] == input[i+1]) {
                        sum += int.Parse(""+input[i]);
                    }
                }
            }
            return $"{sum}";
        }

        public string SolveP2(string input) {
            int sum = 0;
            int step = input.Length / 2;
            for (int i = 0; i < input.Length; i++) {
                if (i + step >= input.Length) {
                    if (input[i] == input[i- input.Length + step]) {
                        sum += int.Parse("" + input[i]);
                    }
                } else {
                    if (input[i] == input[i + step]) {
                        sum += int.Parse("" + input[i]);
                    }
                }
            }
            return $"{sum}";
        }
    }
}
