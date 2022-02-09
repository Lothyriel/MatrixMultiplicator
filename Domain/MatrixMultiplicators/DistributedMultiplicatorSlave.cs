namespace Domain.MatrixMultiplication
{
    public class DistributedMultiplicatorSlave
    {
        public DistributedMultiplicatorSlave()
        {
            MatrixALines = new();
            MatrixBColumns = new();
        }

        public Dictionary<int , List<double>> MatrixALines { get; }
        public Dictionary<int, List<double>> MatrixBColumns { get; }

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

        public void SyncMatrixData(ref List<double>? line, int xM, ref List<double>? column, int yM)
        {
            if(line is not null)
                MatrixALines[xM] = line;
            else 
                line = MatrixALines[xM];

            if (column is not null)
                MatrixBColumns[yM] = column;
            else
                column = MatrixBColumns[yM];
        }
    }
}