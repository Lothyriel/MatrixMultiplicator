namespace Domain
{
    public class Matrix
    {
        public Matrix(List<List<double>> matrix)
        {
            DoubleMatrix = matrix;
        }

        public int X => DoubleMatrix.Count;
        public int Y => DoubleMatrix[0].Count;
        public List<List<double>> DoubleMatrix { get; }
    }
}