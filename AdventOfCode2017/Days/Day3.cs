using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days {
    /*
     * 37 36 35 34 33 32 31
     * 38 17 16 15 14 13 30
     * 39 18  5  4  3 12 29
     * 40 19  6  1  2 11 28
     * 41 20  7  8  9 10 27
     * 42 21 22 23 24 25 26
     * 43 44 45 46 47 48 49 50
     * ....                 81
     */

    class Day3 : IDay {
        public string SolveP1(string input) {
            int num = int.Parse(input);
            if (num == 1) {
                return "0";
            }

            int square = 1;
            int squareEnd = 1;

            // Determine the square we are on and based on the last number
            for (; squareEnd < num; square += 2, squareEnd = (int) Math.Pow(square, 2))
                ;

            // Calculate the (optimal) distance from this square to one
            int middleDistance = (square - 1) / 2;

            // Get the lowest number of the side we are on
            double posInSquare = squareEnd;
            for (; posInSquare >= num; posInSquare -= (square - 1))
                ;
            // Subtract lowest number of the side from the number we are looking for
            // we basically get the index of the number for this side
            posInSquare = num - posInSquare;

            // Get Distance from optimal distance
            posInSquare = Math.Abs(posInSquare - middleDistance);

            // calculate way
            return "" + posInSquare + middleDistance;
        }

        public string SolveP2(string input) {
            int output = 0;
            int num = int.Parse(input);

            int offset = 500;
            int[,] matrix = new int[offset * 2 + 1, offset * 2 + 1];

            matrix[offset, offset] = 1;

            int steps = 0;
            int x = 0, y = 0;
            while (true) {
                steps++;
                //go right
                for (int r = 1; r <= steps; r++) {
                    x++;
                    output = sumArea(matrix, offset, x, y);
                    if (output > num) { return "" + output; }
                    matrix[x + offset, y + offset] = output;
                }

                //go up
                for (int u = 1; u <= steps; u++) {
                    y--;
                    output = sumArea(matrix, offset, x, y);
                    if (output > num) { return "" + output; }
                    matrix[x + offset, y + offset] = output;
                }

                steps++;
                //go left
                for (int l = 1; l <= steps; l++) {
                    x--;
                    output = sumArea(matrix, offset, x, y);
                    if (output > num) { return "" + output; }
                    matrix[x + offset, y + offset] = output;
                }
                //go down
                for (int d = 1; d <= steps; d++) {
                    y++;
                    output = sumArea(matrix, offset, x, y);
                    if (output > num) { return "" + output; }
                    matrix[x + offset, y + offset] = output;
                }
            }
        }

        private int sumArea(int[,] matrix, int offset, int x, int y) {
            int sum = 0;
            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    sum += matrix[offset + i + x, offset + j + y];
                }
            }
            return sum;
        }

        private void printMatrix(int[,] matrix) {
            Console.WriteLine("Printing matrix:");
            int matrixLength = matrix.GetLength(0);
            for (int i = 0; i < matrixLength; i++) {
                for (int j = 0; j < matrixLength; j++) {
                    Console.Write(matrix[i, j].ToString("D5"));
                    if (j + 1 < matrixLength) {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
