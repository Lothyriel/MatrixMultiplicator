using Domain.Matrices;
using System.Diagnostics;

namespace Domain.MatrixMultiplication
{
    public class LocalMultiplicator
    {
        public LocalMultiplicator(Matrix matrixA, Matrix matrixB)
        {
            MatrixA = matrixA;
            MatrixB = matrixB;
            Assert();
        }

        public Matrix MatrixA { get; }
        public Matrix MatrixB { get; }
        public Action<int, int>? ResultHandler { get; set; }
        public Action<TimeSpan>? ExitHandler { get; set; }
        public Matrix? Result { get; private set; }

        public void MultiplySingleThreaded()
        {
            var sw = Stopwatch.StartNew();
            var resultMatrix = new List<List<double>>(MatrixA.X);
            for (int i = 0; i < MatrixA.X; i++)
            {
                resultMatrix.Add(GetResultLine(i));
            }
            sw.Stop();
            Result = new Matrix(resultMatrix);
            ExitHandler!(sw.Elapsed);
        }
        public void MultiplyMultiThreaded()
        {
            var sw = Stopwatch.StartNew();

            var resultMatrix = new Dictionary<int, List<double>>(MatrixA.X);
            int i = 0;
            Parallel.For(i, MatrixA.X, (i) => resultMatrix.Add(i, GetResultLine(i)));
            sw.Stop();
            Result = Matrix.Sorted(resultMatrix);

            ExitHandler!(sw.Elapsed);
        }
        private List<double> GetResultLine(int i)
        {
            var resultLine = new List<double>(MatrixB.Y);
            var line = MatrixA.InnerMatrix[i];

            for (int x = 0; x < MatrixB.Y; x++)
            {
                double result = 0;
                for (int y = 0; y < MatrixA.X; y++)
                {
                    double numberLineA = line[y]!;
                    double numberColumnB = MatrixB.InnerMatrix[y][x]!;
                    result += numberLineA * numberColumnB;
                }
                resultLine.Add(result);
                ResultHandler!(x, i);
            }
            return resultLine;
        }
        private void Assert()
        {
            if (!(MatrixA.Y == MatrixB.X))
                throw new ArgumentException("Cannot multiply these Matrices");
        }
    }
}