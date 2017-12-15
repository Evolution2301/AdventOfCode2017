using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    class Day7 : IDay {
        public string SolveP1(string input) {
            ILookup<string, Program> programs = MapInput(input);

            return GetRoot(programs).Name;
        }

        public string SolveP2(string input) {
            ILookup<string, Program> programs = MapInput(input);

            Program root = GetRoot(programs);
            CalculateTowerWeight(root);

            Program imbalanced = FindImbalancedTower(root);
            int weight = imbalanced.Childs[0].TowerWeight;
            int correctWeight = 0;
            for (int i = 1; i < imbalanced.Childs.Count - 1; i++) {
                if (imbalanced.Childs[i].TowerWeight != weight && imbalanced.Childs[i + 1].TowerWeight != weight) {
                    int diff = weight - imbalanced.Childs[i].TowerWeight;
                    correctWeight = imbalanced.Childs[0].Weight - diff;
                } else if (imbalanced.Childs[i].TowerWeight == weight && imbalanced.Childs[i + 1].TowerWeight != weight) {
                    int diff = imbalanced.Childs[i + 1].TowerWeight - weight;
                    correctWeight = imbalanced.Childs[i + 1].Weight - diff;
                } else if (imbalanced.Childs[i].TowerWeight != weight && imbalanced.Childs[i + 1].TowerWeight == weight) {
                    int diff = imbalanced.Childs[i].TowerWeight - weight;
                    correctWeight = imbalanced.Childs[i].Weight - diff;
                }
            }
            return "" + correctWeight;
        }

        private Program FindImbalancedTower(Program program) {
            if (program.Childs.Count == 0) {
                return null;
            }
            foreach (Program child in program.Childs) {
                Program imbalanced = FindImbalancedTower(child);
                if (imbalanced != null) {
                    return imbalanced;
                }
            }

            int weight = program.Childs[0].TowerWeight;
            for (int i = 1; i < program.Childs.Count; i++) {
                if (program.Childs[i].TowerWeight != weight) {
                    return program;
                }
            }
            return null;
        }

        private Program GetRoot(ILookup<string, Program> programs) {
            Program program = programs.First().First();

            while (program.Parent != null) {
                program = program.Parent;
            }
            return program;
        }

        private ILookup<string, Program> MapInput(string input) {
            List<Program> programs = new List<Program>();
            string[] progLines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string prog in progLines) {
                string line = prog.Replace(" ", "");
                string[] split = line.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                Program program = new Program();
                if (split.Length > 1) {
                    program.Children = split[1];
                }
                program.Name = split[0].Substring(0, split[0].IndexOf('('));
                program.Weight = int.Parse(split[0].Substring(split[0].IndexOf('(') + 1, split[0].IndexOf(')') - (split[0].IndexOf('(') + 1)));
                programs.Add(program);
            }

            ILookup<string, Program> progs = programs.ToLookup(p => p.Name, p => p);

            foreach (Program program in programs) {
                if (!String.IsNullOrWhiteSpace(program.Children)) {
                    string[] children = program.Children.Split(',');
                    foreach (string child in children) {
                        progs[child].First().Parent = program;
                        program.Childs.Add(progs[child].First());
                    }
                }
            }

            return progs;
        }

        private int CalculateTowerWeight(Program program) {
            int weight = program.Weight;
            foreach (Program child in program.Childs) {
                weight += CalculateTowerWeight(child);
            }
            program.TowerWeight = weight;
            return program.TowerWeight;
        }

        class Program {
            public string Name { get; set; }
            public int Weight { get; set; }
            public int TowerWeight { get; set; }
            public Program Parent { get; set; }
            public string Children { get; set; }
            public List<Program> Childs = new List<Program>();

            public override string ToString() {
                return $"{Name} ({Weight}) => {Children} | P={Parent?.Name} | TW={TowerWeight}";
            }
        }
    }
}
