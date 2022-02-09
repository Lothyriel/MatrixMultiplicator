using Domain.Exceptions;
using Domain.MatrixOperations;
using System.Globalization;

namespace Domain.Matrices
{
    public class MatrixReader
    {
        private IncompleteMatrix Matrix { get; }
        public MatrixReader(string path)
        {
            Matrix = Resolve(path);
            Assert(Matrix);
        }

        public Matrix Build()
        {
            return new Matrix(Matrix);
        }

        public static void Assert(IncompleteMatrix Matrix)
        {
            if (Matrix.InnerMatrix.InnerArray.Any(line => line.Length != Matrix[0].Length))
                throw new InvalidMatrix();
        }

        private static IncompleteMatrix Resolve(string path)
        {
            var matrix = new IncompleteMatrix();
            using var reader = File.OpenText(path);
            string nextLine;
            int x = 0;
            while ((nextLine = reader.ReadLine()!) != null)
            {
                var line = new IncompleteArray();
                var lines = nextLine.Split();
                for (int y = 0; y < lines.Length; y++)
                {
                    line[y] = double.Parse(lines[y], CultureInfo.InvariantCulture);
                }
                matrix[x++] = line;
            }
            return matrix;
        }
    }
}