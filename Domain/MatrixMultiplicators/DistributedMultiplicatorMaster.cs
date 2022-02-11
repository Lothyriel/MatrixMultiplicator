using Domain.Connection;
using Domain.ExtensionMethods;
using Domain.Matrices;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Domain.MatrixMultiplicators
{
    public class DistributedMultiplicatorMaster
    {
        public DistributedMultiplicatorMaster(Matrix matrixA, Matrix matrixB)
        {
            MatrixA = matrixA;
            MatrixB = matrixB;
            Result = new double[matrixA.X, matrixB.Y];
            AlreadySentLines = new();
            AlreadySentColumns = new();
            AlreadyReceivedResults = new();
            ClientsData = new();
            ClientDataIndex = 0;
        }
        public double[,] Result { get; }

        public List<ClientData> ClientsData { get; }
        public Matrix MatrixA { get; }
        public Matrix MatrixB { get; }
        private int ClientDataIndex { get; set; }
        private ConcurrentDictionary<ClientData, HashSet<int>> AlreadySentLines { get; }
        private ConcurrentDictionary<ClientData, HashSet<int>> AlreadySentColumns { get; }
        private ConcurrentDictionary<(int, int), byte> AlreadyReceivedResults { get; }

        private static readonly object Lock = new();  

        public bool Ended() => MatrixA.X * MatrixB.Y == AlreadyReceivedResults.Count;

        public void CloseConnections()
        {
            foreach (var clientData in ClientsData)
            {
                Task.Run(() => EndConnection(clientData));
            }

            static void EndConnection(ClientData clientData)
            {
                ServerMatrixConnection.TerminateConnection(clientData);
                clientData.Client.Close();
                clientData.Stream.Close();
            }
        }

        public void AddClientData(ClientData cientData)
        {
            lock (Lock)
            {
                ClientsData.Add(cientData);
            }
        }

        public void SendMultiplicationRequests()
        {
            Task.Run(BeginSending);

            void BeginSending()
            {
                var tasks = new Task[MatrixA.X * MatrixB.Y];
                int x = 0;
                int y = 0;

                for (x = 0; x < MatrixA.X; x++)
                {
                    for (y = 0; y < MatrixB.Y; y++)
                    {
                        var copyX = x;  //avoiding lambda closures
                        var copyY = y;
                        tasks[y + (MatrixB.Y * x)] = Task.Run(() => DistributedMultiplication(copyX, copyY, GetNextClientData()));
                    }
                }
                Task.WaitAll(tasks);
            }
        }

        public void UpdateResult(MultiplicationResult multiplicationResult)
        {
            var x = multiplicationResult.Xm;
            var y = multiplicationResult.Ym;
            Result[x, y] = multiplicationResult.Result;
            AlreadyReceivedResults[(x, y)] = byte.MinValue;
        }
        public void DistributedMultiplication(int x, int y, ClientData clientData)
        {
            List<double>? line = null;
            List<double>? column = null;

            if (!AlreadySentLines.TryGetValue(clientData, out var clientsLine) || !clientsLine.TryGetValue(x, out _))
            {
                clientsLine ??= new();

                line = MatrixA.InnerMatrix[x];

                AlreadySentLines[clientData] = clientsLine.AddAndReturn(x);
            }

            if (!AlreadySentColumns.TryGetValue(clientData, out var clientsColumns) || !clientsColumns.TryGetValue(y, out _))
            {
                clientsColumns ??= new();

                column = MatrixB.GetColumn(y);

                AlreadySentColumns[clientData] = clientsColumns.AddAndReturn(y);
            }
            Debug.WriteLine($"Enviando a request {x} {y}");
            ServerMatrixConnection.SendRequest(new MultiplicationRequest(line, x, column, y), clientData);
        }

        public ClientData GetNextClientData()
        {
            lock (Lock)
            {
                if (ClientDataIndex == ClientsData.Count)
                    ClientDataIndex = 0;

                return ClientsData[ClientDataIndex++];
            }
        }
    }
}