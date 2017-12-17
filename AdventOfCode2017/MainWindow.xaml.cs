using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdventOfCode2017.Days;

namespace AdventOfCode2017 {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void bt_click(object sender, RoutedEventArgs e) {
            string input = input_tb.Text;
            string output = "";

            switch ((sender as Button).Name) {
                case "d1p1":
                    output = new Day1().SolveP1(input);
                    break;
                case "d1p2":
                    output = new Day1().SolveP2(input);
                    break;
                case "d2p1":
                    output = new Day2().SolveP1(input);
                    break;
                case "d2p2":
                    output = new Day2().SolveP2(input);
                    break;
                case "d3p1":
                    output = new Day3().SolveP1(input);
                    break;
                case "d3p2":
                    output = new Day3().SolveP2(input);
                    break;
                case "d4p1":
                    output = new Day4().SolveP1(input);
                    break;
                case "d4p2":
                    output = new Day4().SolveP2(input);
                    break;
                case "d5p1":
                    output = new Day5().SolveP1(input);
                    break;
                case "d5p2":
                    output = new Day5().SolveP2(input);
                    break;
                case "d6p1":
                    output = new Day6().SolveP1(input);
                    break;
                case "d6p2":
                    output = new Day6().SolveP2(input);
                    break;
                case "d7p1":
                    output = new Day7().SolveP1(input);
                    break;
                case "d7p2":
                    output = new Day7().SolveP2(input);
                    break;
                case "d8p1":
                    output = new Day8().SolveP1(input);
                    break;
                case "d8p2":
                    output = new Day8().SolveP2(input);
                    break;
                case "d9p1":
                    output = new Day9().SolveP1(input);
                    break;
                case "d9p2":
                    output = new Day9().SolveP2(input);
                    break;
                case "d10p1":
                    output = new Day10().SolveP1(input);
                    break;
                case "d10p2":
                    output = new Day10().SolveP2(input);
                    break;
                case "d11p1":
                    output = new Day11().SolveP1(input);
                    break;
                case "d11p2":
                    output = new Day11().SolveP2(input);
                    break;
                case "d12p1":
                    output = new Day12().SolveP1(input);
                    break;
                case "d12p2":
                    output = new Day12().SolveP2(input);
                    break;
                case "d13p1":
                    output = new Day13().SolveP1(input);
                    break;
                case "d13p2":
                    output = new Day13().SolveP2(input);
                    break;
                case "d14p1":
                    output = new Day14().SolveP1(input);
                    break;
                case "d14p2":
                    output = new Day14().SolveP2(input);
                    break;
                case "d15p1":
                    output = new Day15().SolveP1(input);
                    break;
                case "d15p2":
                    output = new Day15().SolveP2(input);
                    break;
                case "d16p1":
                    output = new Day16().SolveP1(input);
                    break;
                case "d16p2":
                    output = new Day16().SolveP2(input);
                    break;
                case "d17p1":
                    output = new Day17().SolveP1(input);
                    break;
                case "d17p2":
                    output = new Day17().SolveP2(input);
                    break;
                case "d18p1":
                    output = new Day18().SolveP1(input);
                    break;
                case "d18p2":
                    output = new Day18().SolveP2(input);
                    break;
                case "d19p1":
                    output = new Day19().SolveP1(input);
                    break;
                case "d19p2":
                    output = new Day19().SolveP2(input);
                    break;
                case "d20p1":
                    output = new Day20().SolveP1(input);
                    break;
                case "d20p2":
                    output = new Day20().SolveP2(input);
                    break;
                case "d21p1":
                    output = new Day21().SolveP1(input);
                    break;
                case "d21p2":
                    output = new Day21().SolveP2(input);
                    break;
                case "d22p1":
                    output = new Day22().SolveP1(input);
                    break;
                case "d22p2":
                    output = new Day22().SolveP2(input);
                    break;
                case "d23p1":
                    output = new Day23().SolveP1(input);
                    break;
                case "d23p2":
                    output = new Day23().SolveP2(input);
                    break;
                case "d24p1":
                    output = new Day24().SolveP1(input);
                    break;
                case "d24p2":
                    output = new Day24().SolveP2(input);
                    break;
            }

            output_tb.Text = output;
        }
    }
}
