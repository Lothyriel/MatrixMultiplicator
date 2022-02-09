using Domain.ExtensionMethods;
using Domain.MatrixOperations;

namespace Domain.MatrixMultiplication
{
    public class DistributedMultiplicatorSlave
    {
        public DistributedMultiplicatorSlave()
        {
            MatrixA = new();
            MatrixB = new();
        }

        public IncompleteMatrix MatrixA { get; }
        public IncompleteMatrix MatrixB { get; }

        public double MultiplyLineByColumn(double[]? line, int xM, double[]? column, int yM)
        {
            SyncMatrixData(ref line, xM, ref column, yM);

            double result = 0;
            for (int y = 0; y < line!.Length; y++)
            {
                var numeroLinhaA = line[y];
                var numeroColunaB = column![y];
                result += numeroLinhaA * numeroColunaB;
            }
            return result;
        }

        public void SyncMatrixData(ref double[]? line, int xM, ref double[]? column, int yM)
        {
            line ??= MatrixA[xM].InnerArray.Denullify();
            column ??= MatrixB.GetColumn(yM).Denullify();
        }
    }
}