using Domain.Connection;
using Domain.Matrices;

namespace Domain.MatrixMultiplicators
{
    public class DistributedMultiplicatorServer
    {
        public DistributedMultiplicatorServer(Matrix matrixA, Matrix matrixB, MatrixConnection matrixConnection, List<string> ips)
        {
            MatrixA = matrixA;
            MatrixB = matrixB;
            MatrixConnection = matrixConnection;
            IPs = ips;
            Result = new double[matrixA.X, matrixB.Y];
            AlreadySentData = new();
        }

        public Matrix MatrixA { get; }
        public Matrix MatrixB { get; }
        public double[,] Result { get; }
        public Dictionary<(int, int), HashSet<string>> AlreadySentData { get; }
        public MatrixConnection MatrixConnection { get; }
        public List<string> IPs { get; }

        public double[,] MultiplyDistributedAsync()
        {
            var tasks = new Task[MatrixA.X];
            var ips = GetNextIp().GetEnumerator();
            for (int x = 0; x < MatrixA.X; x++)
            {
                for (int y = 0; y < MatrixB.Y; y++)
                {
                    tasks[x + y] = Task.Run(() => DistributedMultiplication(x, y, ips.Current));
                    ips.MoveNext();
                }
            }
            Task.WaitAll(tasks);
            return Result;
        }

        public void UpdateResult(MultiplicationResult multiplicationResult)
        {
            var x = multiplicationResult.Xm;
            var y = multiplicationResult.Ym;
            Result[x, y] = multiplicationResult.Result;
        }

        private IEnumerable<string> GetNextIp()
        {
            int i = 0;
            while (true)
            {
                if (i == IPs.Count)
                    i = 0;
                yield return IPs[i++];
            }
        }

        private void DistributedMultiplication(int x, int y, string ip)
        {
            List<double>? line = null;
            List<double>? column = null;
            if (!AlreadySentData.TryGetValue((x, y), out var clients) || !clients.TryGetValue(ip, out _))
            {
                clients ??= new();

                line = MatrixA.DoubleMatrix[x];
                column = MatrixB.GetColumn(y);

                AlreadySentData[(x, y)] = clients.AddAndReturn(ip);
            }
            MatrixConnection.Send(new MultiplicationRequest(line, x, column, y), ip);
        }
    }
}