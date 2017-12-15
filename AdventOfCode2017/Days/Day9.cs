using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day9 : IDay {
        public string SolveP1(string input) {
            string cleaned = CleanInput(input);

            long level = 0;
            long sum = 0;
            foreach (char c in cleaned) {
                if (c == '{') {
                    level++;
                } else if (c == '}') {
                    sum += level;
                    level--;
                }
            }

            return "" + sum;
        }

        private string CleanInput(string input) {
            string clean = "";

            input = input.Replace("!!", "");

            bool negateNext = false;
            bool inGarbage = false;
            foreach (char c in input) {
                if (!inGarbage) {
                    if (c != '<') {
                        clean += c;
                    } else if (c == '<') {
                        inGarbage = true;
                    }
                } else {
                    if (negateNext) {
                        negateNext = false;
                    } else {
                        if (c == '!') {
                            negateNext = true;
                        } else if (c == '>') {
                            inGarbage = false;
                        }
                    }
                }
            }

            return clean;
        }

        public string SolveP2(string input) {
            return "" + CountCleaned(input);
        }


        private int CountCleaned(string input) {
            int cleaned = 0;

            input = input.Replace("!!", "");

            bool negateNext = false;
            bool inGarbage = false;
            foreach (char c in input) {
                if (!inGarbage) {
                    if (c == '<') {
                        inGarbage = true;
                    }
                } else {
                    if (negateNext) {
                        //if (c == '!' || c == '>') {
                        negateNext = false;
                        //}
                    } else {
                        if (c == '!') {
                            negateNext = true;
                        } else if (c == '>') {
                            inGarbage = false;
                        } else {
                            cleaned++;
                        }
                    }
                }
            }

            return cleaned;
        }

    }
}
