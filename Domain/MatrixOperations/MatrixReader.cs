using System.Globalization;

namespace Domain.Matrices
{
    public class MatrixReader
    {
        public static Matrix Resolve(string path)
        {
            var matrix = new List<List<double>>();
            using var reader = File.OpenText(path);
            string nextLine;
            while ((nextLine = reader.ReadLine()!) != null)
            {
                var line = new List<double>();
                foreach (var item in nextLine.Split())
                {
                    line.Add(double.Parse(item, CultureInfo.InvariantCulture));
                }
                matrix.Add(line);
            }
            return new Matrix(matrix);
        }
    }
}