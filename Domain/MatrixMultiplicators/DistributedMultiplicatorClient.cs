using Domain.Matrices;

namespace Domain.MatrixMultiplication
{
    public class DistributedMultiplicatorClient
    {
        public DistributedMultiplicatorClient()
        {
            MatrixA = new();
            MatrixB = new();
        }

        public Matrix MatrixA { get; }
        public Matrix MatrixB { get; }

        public double MultiplyLineByColumn(List<double>? line, int xM, List<double>? column, int yM)
        {
            SyncMatrixData(ref line, xM, ref column, yM);

            double result = 0;
            for (int y = 0; y < line!.Count; y++)
            {
                var numeroLinhaA = line[y];
                var numeroColunaB = column![y];
                result += numeroLinhaA * numeroColunaB;
            }
            return result;
        }

        private void SyncMatrixData(ref List<double>? line, int xM, ref List<double>? column, int yM)
        {
            line ??= MatrixA.DoubleMatrix[xM];
            column ??= MatrixB.GetColumn(yM);
        }
    }
}