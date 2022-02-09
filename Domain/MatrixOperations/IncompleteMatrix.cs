namespace Domain.MatrixOperations
{
    public class IncompleteMatrix : IncompleteMatrix<double?> { }
    public class IncompleteArray : IncompleteArray<double?> { }
    public class IncompleteMatrix<T>
    {
        public IncompleteMatrix(int x = 2, int y = 2)
        {
            InnerMatrix = new(x);
            Fill(0, x, y);
        }

        public int X => InnerMatrix.Length;
        public int Y => InnerMatrix[0]!.Length;

        private readonly IncompleteArray<IncompleteArray<T>> InnerMatrix;

        public IncompleteArray<T> this[int key]
        {
            get => GetValue(key);
            set => InnerMatrix[key] = value;
        }

        public T[] GetColumn(int columnNumber)
        {
            return Enumerable.Range(0, X).Select(x => InnerMatrix[x][columnNumber]).ToArray();
        }

        private IncompleteArray<T> GetValue(int key)
        {
            if (InnerMatrix.Length - 1 < key)
                Grow();

            return InnerMatrix[key]!;
        }

        private void Grow()
        {
            Array.Resize(ref InnerMatrix.InnerArray, InnerMatrix.Length *= 2);
            Fill(InnerMatrix.Length / 2, InnerMatrix.Length, InnerMatrix.Length);
        }

        private void Fill(int start, int end, int lineSize)
        {
            for (int i = start; i < end; i++)
            {
                InnerMatrix[i] = new(lineSize);
            }
        }
    }
    public class IncompleteArray<T>
    {
        public IncompleteArray(int size = 2)
        {
            Length = size;
            InnerArray = new T[size];
        }

        public T[] InnerArray;

        public int Length { get; set; }

        public T this[int key]
        {
            get => InnerArray[key];
            set => SetValue(key, value);
        }

        public static implicit operator T[](IncompleteArray<T> inArray) => inArray.InnerArray;

        private void SetValue(int key, T value)
        {
            if (InnerArray.Length - 1 < key)
                Grow();

            InnerArray[key] = value;
        }

        private void Grow()
        {
            Array.Resize(ref InnerArray, Length *= 2);
        }
    }
}
