using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade_design_pattern
{
    // Exposing several components through a single interface
    // Balancing complexity and presentation/usability
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] a = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            List<List<int>> list = generateSquare(3);
            
            foreach(List<int> list3 in list)
            {
                string s = "";
                foreach(int i in list3)
                {
                    s += i.ToString();
                }
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }
        static List<List<int>> generateSquare(int n)
        {
            int[,] magicSquare = new int[n, n];
            int i = n / 2;
            int j = n - 1;

            for (int num = 1; num <= n * n;)
            {
                if (i == -1 && j == n) 
                {
                    j = n - 2;
                    i = 0;
                }
                else
                {
                    if (j == n)
                        j = 0;
                    if (i < 0)
                        i = n - 1;
                }
                if (magicSquare[i, j] != 0)
                {
                    j -= 2;
                    i++;
                    continue;
                }
                else
                    magicSquare[i, j] = num++;
                j++;
                i--;
            }
            
            List<List<int>> list = new List<List<int>>();
            for (int x = 0; x < magicSquare.GetLength(0); x++)
            {
                List<int> list2 = new List<int>();
                for (int y = 0; y < magicSquare.GetLength(0); y++)
                {
                    list2.Add(magicSquare[x, y]);
                }
                list.Add(list2);
            }

            return list;
        }
    }
    public class Generator
    {
        private static readonly Random random = new Random();

        public List<int> Generate(int count)
        {
            return Enumerable.Range(0, count)
              .Select(_ => random.Next(1, 6))
              .ToList();
        }
    }

    public class Splitter
    {
        public List<List<int>> Split(List<List<int>> array)
        {
            var result = new List<List<int>>();

            var rowCount = array.Count;
            var colCount = array[0].Count;

            // get the rows
            for (int r = 0; r < rowCount; ++r)
            {
                var theRow = new List<int>();
                for (int c = 0; c < colCount; ++c)
                    theRow.Add(array[r][c]);
                result.Add(theRow);
            }

            // get the columns
            for (int c = 0; c < colCount; ++c)
            {
                var theCol = new List<int>();
                for (int r = 0; r < rowCount; ++r)
                    theCol.Add(array[r][c]);
                result.Add(theCol);
            }

            // now the diagonals
            var diag1 = new List<int>();
            var diag2 = new List<int>();
            for (int c = 0; c < colCount; ++c)
            {
                for (int r = 0; r < rowCount; ++r)
                {
                    if (c == r)
                        diag1.Add(array[r][c]);
                    var r2 = rowCount - r - 1;
                    if (c == r2)
                        diag2.Add(array[r][c]);
                }
            }

            result.Add(diag1);
            result.Add(diag2);

            return result;
        }
    }

    public class Verifier
    {
        public bool Verify(List<List<int>> array)
        {
            if (!array.Any()) return false;

            var expected = array.First().Sum();

            return array.All(t => t.Sum() == expected);
        }
    }

    public class MagicSquareGenerator
    {
        public List<List<int>> Generate(int size)
        {
            Generator generator = new Generator();
            Splitter splitter = new Splitter();
            Verifier verifier = new Verifier();
            List<List<int>> matrix = new List<List<int>>();
            bool magic = false;
            while (magic == false)
            {
                for (int i = 0; i < size; i++)
                {
                    List<int> s = generator.Generate(size);
                    matrix.Add(s);
                }
                var splitted = splitter.Split(matrix);
                magic = verifier.Verify(splitted);
            }
            return matrix;
        }
    }
}
