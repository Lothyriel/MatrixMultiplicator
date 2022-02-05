using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MatrixReader
    {
        private List<List<double>> Matrix { get; }
        public MatrixReader(string path)
        {
            Matrix = Resolve(path);
            Assert();
        }

        public Matrix Build()
        {
            return new Matrix(Matrix);
        }

        private void Assert()
        {
            if (Matrix.Any(line => line.Count != Matrix[0].Count))
                throw new Exception("Invalid Matrix, lines don't have the same number of elements");
        }

        private List<List<double>> Resolve(string path)
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