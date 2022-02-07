using Domain.Matrices;

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

        public Matrix MultiplySingleThreaded()
        {
            var resultMatrix = new List<List<double>>(MatrixA.X);
            for (int i = 0; i < MatrixA.X; i++)
            {
                resultMatrix.Add(GetResultLine(i));
            }
            return new Matrix(resultMatrix);
        }
        public Matrix MultiplyMultiThreaded()
        {
            var resultMatrix = new List<List<double>>(MatrixA.X);
            int i = 0;
            Parallel.For(i, MatrixA.X, (i) => resultMatrix.Add(GetResultLine(i)));
            return new Matrix(resultMatrix);
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
                    var numberLineA = line[y];
                    var numberColumnB = MatrixB.InnerMatrix[y][x];
                    result += numberLineA * numberColumnB;
                }
                resultLine.Add(result);
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