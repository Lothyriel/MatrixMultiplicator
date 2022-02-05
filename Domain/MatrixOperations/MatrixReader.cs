using Domain.Exceptions;
using System.Globalization;

namespace Domain.Matrices
{
    public class MatrixReader
    {
        private List<List<double>> Matrix { get; }
        public MatrixReader(string path)
        {
            Matrix = Resolve(path);
            Assert(Matrix);
        }

        public Matrix Build()
        {
            return new Matrix(Matrix);
        }

        public static void Assert(List<List<double>> Matrix)
        {
            if (Matrix.Any(line => line.Count != Matrix[0].Count))
                throw new InvalidMatrix();
        }

        private static List<List<double>> Resolve(string path)
        {
            var matrix = new List<List<double>>();
            using var reader = File.OpenText(path);
            string nextLine;
            while ((nextLine = reader.ReadLine()!) != null)
            {
                var line = new List<double>();
                foreach (var strNumber in nextLine.Split())
                {
                    line.Add(double.Parse(strNumber, CultureInfo.InvariantCulture));
                }
                matrix.Add(line);
            } 
            return matrix;
        }
    }
}