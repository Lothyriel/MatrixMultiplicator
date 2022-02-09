using Domain.MatrixOperations;
using System.Text;

namespace Domain.Matrices
{
    public class Matrix
    {
        public Matrix(IncompleteMatrix matrix)
        {
            InnerMatrix = matrix;
        }

        public int X => InnerMatrix.X;
        public int Y => InnerMatrix.Y;
        public IncompleteMatrix InnerMatrix { get; }

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

        public double?[] GetColumn(int column)
        {
            return InnerMatrix.GetColumn(column);
        }
    }
}