using Domain.Application;
using Domain.Connection;
using Domain.ExtensionMethods;
using Domain.Matrices;

namespace Domain.MatrixMultiplicators
{
    public class DistributedMultiplicatorMaster
    {
        public DistributedMultiplicatorMaster(Matrix matrixA, Matrix matrixB, Master master)
        {
            MatrixA = matrixA;
            MatrixB = matrixB;
            MatrixConnection = master.MatrixConnection;
            IPs = master.IPs;
            Result = new double[matrixA.X, matrixB.Y];
            AlreadySentLines = new();
            AlreadySentColumns = new();
            AlreadyReceived = new();
        }
        private MatrixConnection MatrixConnection { get; }
        private Matrix MatrixA { get; }
        private Matrix MatrixB { get; }
        private List<string> IPs { get; }
        private double[,] Result { get; }
        private Dictionary<int, HashSet<string>> AlreadySentLines { get; }
        private Dictionary<int, HashSet<string>> AlreadySentColumns { get; }
        private HashSet<(int, int)> AlreadyReceived { get; }

        public bool Ended() => MatrixA.X * MatrixB.Y == AlreadyReceived.Count;

        public void SendMultiplicationRequests()
        {
            var tasks = new Task[MatrixA.X * MatrixB.Y];
            var ips = GetNextIp().GetEnumerator();
            int x = 0;
            int y = 0;

            for (x = 0; x < MatrixA.X; x++)
            {
                for (y = 0; y < MatrixB.Y; y++)
                {
                    ips.MoveNext();
                    var copyX = x;  //avoiding lambda closures
                    var copyY = y;
                    var ipCopy = ips.Current;
                    tasks[y + (MatrixB.Y * x)] = Task.Run(() => DistributedMultiplication(copyX, copyY, ipCopy));
                }
            }
            Task.WaitAll(tasks);
        }

        private void DistributedMultiplication(int x, int y, string ip)
        {
            List<double>? line = null;
            List<double>? column = null;

            if (!AlreadySentLines.TryGetValue(x, out var clientsLine) || !clientsLine.TryGetValue(ip, out _))
            {
                clientsLine ??= new();

                line = MatrixA.InnerMatrix[x];

                AlreadySentLines[x] = clientsLine.AddAndReturn(ip);
            }

            if (!AlreadySentColumns.TryGetValue(y, out var clientsColumns) || !clientsLine.TryGetValue(ip, out _))
            {
                clientsColumns ??= new();

                column = MatrixB.GetColumn(y);

                AlreadySentColumns[y] = clientsColumns.AddAndReturn(ip);
            }

            MatrixConnection.Send(new MultiplicationRequest(line?.ToArray(), x, column?.ToArray(), y), ip);
        }

        public void UpdateResult(MultiplicationResult multiplicationResult)
        {
            var x = multiplicationResult.Xm;
            var y = multiplicationResult.Ym;
            Result[x, y] = multiplicationResult.Result;
            AlreadyReceived.Add((x, y));
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
    }
}