using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day4 : IDay {
        public string SolveP1(string input) {
            string[] phrases = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int validPhrases = 0;

            foreach (string phrase in phrases) {
                string[] words = phrase.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                HashSet<string> knownWords = new HashSet<string>();
                bool validPhrase = true;
                foreach (string word in words) {
                    if (!knownWords.Contains(word)) {
                        knownWords.Add(word);
                    } else {
                        validPhrase = false;
                        break;
                    }
                }
                if (validPhrase) {
                    validPhrases++;
                }
            }
            return "" + validPhrases;
        }

        public string SolveP2(string input) {
            string[] phrases = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int validPhrases = 0;

            foreach (string phrase in phrases) {
                string[] words = phrase.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                HashSet<string> knownWords = new HashSet<string>();
                bool validPhrase = true;
                foreach (string word in words) {
                    string orderedWord = OrderWord(word);
                    if (!knownWords.Contains(orderedWord)) {
                        knownWords.Add(orderedWord);
                    } else {
                        validPhrase = false;
                        break;
                    }
                }
                if (validPhrase) {
                    validPhrases++;
                }
            }
            return "" + validPhrases;
        }

        private string OrderWord(string word) {
            StringBuilder orderedWord = new StringBuilder();
            word = word.ToLower();
            for (char c = 'a'; c <= 'z'; c++) {
                for (int i = 0; i < word.Length; i++) {
                    if (word[i] == c) {
                        orderedWord.Append(c);
                    }
                }
            }
            return orderedWord.ToString();
        }
    }
}