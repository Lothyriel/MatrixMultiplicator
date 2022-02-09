using Domain.Matrices;
using Domain.MatrixOperations;

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
            var resultMatrix = new IncompleteMatrix(MatrixA.X, MatrixB.Y);
            for (int i = 0; i < MatrixA.X; i++)
            {
                resultMatrix[i] = GetResultLine(i);
            }
            return new Matrix(resultMatrix);
        }
        public Matrix MultiplyMultiThreaded()
        {
            var resultMatrix = new IncompleteMatrix(MatrixA.X, MatrixB.Y);
            int i = 0;
            Parallel.For(i, MatrixA.X, (i) => resultMatrix[i] = GetResultLine(i));
            return new Matrix(resultMatrix);
        }
        private IncompleteArray GetResultLine(int i)
        {
            var resultLine = new IncompleteArray(MatrixB.Y);
            var line = MatrixA.InnerMatrix[i];

            for (int x = 0; x < MatrixB.Y; x++)
            {
                double result = 0;
                for (int y = 0; y < MatrixA.X; y++)
                {
                    double numberLineA = (double)line[y]!;
                    double numberColumnB = (double)MatrixB.InnerMatrix[y][x]!;
                    result += numberLineA * numberColumnB;
                }
                resultLine[x] = result;
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