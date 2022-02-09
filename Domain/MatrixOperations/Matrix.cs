using Domain.Exceptions;
using System.Text;

namespace Domain.Matrices
{
    public class Matrix
    {
        public Matrix(List<List<double>> matrix)
        {
            Assert(matrix);
            InnerMatrix = matrix;
        }

        public int X => InnerMatrix.Count;
        public int Y => InnerMatrix[0].Count;
        public List<List<double>> InnerMatrix { get; }

        public string SaveFile()
        {
            var path = $"Matrix{X}x{Y}.txt";
            using var textWriter = new StreamWriter(path, false, Encoding.UTF8, 65536);
            for (int x = 0; x < X; x++)
            {
                var sb = new StringBuilder();
                for (int y = 0; y < Y; y++)
                {
                    sb.Append($"{InnerMatrix[x][y]:0.0000} ");
                }
                textWriter.WriteLine(sb);
            }
            return path;
        }

        public List<double> GetColumn(int column)
        {
            return Enumerable.Range(0, X).Select(x => InnerMatrix[x][column]).ToList();
        }

        public static Matrix Sorted(Dictionary<int, List<double>> resultMatrix)
        {
            return new Matrix(resultMatrix.OrderBy(line => line.Key).Select(line => line.Value).ToList());
        }
        public static void Assert(List<List<double>> matrix)
        {
            if (matrix.Any(line => line.Count != matrix[0].Count))
                throw new InvalidMatrix();
        }
    }
}