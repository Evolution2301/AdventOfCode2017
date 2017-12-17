using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day10 : IDay {

        private readonly IList<int> list;

        public Day10() {
            list = new List<int>();
            for (int i = 0; i < 256; i++) {
                list.Add(i);
            }
        }

        public string SolveP1(string input) {
            List<int> lenghts = new List<int>();
            string[] nums = input.Split(new string[] { Environment.NewLine, ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string i in nums) {
                lenghts.Add(int.Parse(i));
            }

            int currentPos = 0;
            int skipSize = 0;
            foreach (int length in lenghts) {
                List<int> subList = new List<int>();
                for (int x = currentPos; x < currentPos + length; x++) {
                    subList.Add(list[x % list.Count]);
                }
                subList.Reverse();
                for (int x = currentPos; x < currentPos + length; x++) {
                    list[x % list.Count] = subList[x - currentPos];
                }
                currentPos = (currentPos + length + skipSize) % list.Count;
                skipSize++;
            }

            return "" + (list[0] * list[1]);
        }

        public string SolveP2(string input) {
            List<int> lenghts = new List<int>();

            foreach (char c in input.Trim()) {
                lenghts.Add(c);
            }

            lenghts.Add(17);
            lenghts.Add(31);
            lenghts.Add(73);
            lenghts.Add(47);
            lenghts.Add(23);

            int currentPos = 0;
            int skipSize = 0;
            for (int round = 0; round < 64; round++) {
                foreach (int length in lenghts) {
                    List<int> subList = new List<int>();
                    for (int x = currentPos; x < currentPos + length; x++) {
                        subList.Add(list[x % list.Count]);
                    }
                    subList.Reverse();
                    for (int x = currentPos; x < currentPos + length; x++) {
                        list[x % list.Count] = subList[x - currentPos];
                    }
                    currentPos = (currentPos + length + skipSize) % list.Count;
                    skipSize++;
                }
            }

            int[] denseHash = new int[16];
            for (int i = 0; i < list.Count; i++) {
                denseHash[i / 16] ^= list[i];
            }

            string result = "";
            foreach (int i in denseHash) {
                result += i.ToString("X2");
            }
            return result;
        }
    }
}
