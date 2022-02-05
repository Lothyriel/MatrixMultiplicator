namespace Domain.Matrices
{
    public class Matrix
    {
        public Matrix(List<List<double>> matrix)
        {
            DoubleMatrix = matrix;
        }
        public Matrix()
        {
            DoubleMatrix = new();
        }

        public int X => DoubleMatrix.Count;
        public int Y => DoubleMatrix[0].Count;
        public List<List<double>> DoubleMatrix { get; }
        public List<double> GetColumn(int column)
        {
            return Enumerable.Range(0, X).Select(x => DoubleMatrix[x][column]).ToList();
        }
    }
}