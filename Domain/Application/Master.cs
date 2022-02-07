using Domain.Connection;
using Domain.ExtensionMethods;
using Domain.Matrices;
using Domain.MatrixMultiplicators;

namespace Domain.Application
{
    public class Master
    {
        public Master(string ip, Matrix matrixA, Matrix matrixB)
        {
            MatrixConnection = new(ip, 25565);
            IPs = new();
            IsRunning = false;
            Multiplicator = new(matrixA, matrixB, this);
        }

        public MatrixConnection MatrixConnection { get; }
        public List<string> IPs { get; }
        public DistributedMultiplicatorMaster Multiplicator { get; set; }

        private bool IsRunning { get; set; }

        public void Start()
        {
            IsRunning = true;
            Multiplicator.SendMultiplicationRequests();
        }
        public void EndHandler(Action handler)
        {
            Task.Run(() => CheckLoop(handler));

            void CheckLoop(Action handler)
            {
                while (!Multiplicator.Ended())
                {
                    Thread.Sleep(3000);
                }
                IsRunning = false;
                handler();
            }
        }
        public void StartReceivingLoop()
        {
            Task.Run(StartLoop);

            void StartLoop()
            {
                while (IsRunning)
                {
                    var received = MatrixConnection.Receive();

                    if (received.StartsWith("{\"X"))
                        HandleResult(received.Desserialize<MultiplicationResult>());

                    if (received.StartsWith("{\"I"))
                        AddIp(received.Desserialize<ConnectionInfo>());
                }
            }
        }

        private void AddIp(ConnectionInfo connectionInfo)
        {
            IPs.Add(connectionInfo.IP);
        }
        private void HandleResult(MultiplicationResult multiplicationResult)
        {
            Multiplicator!.UpdateResult(multiplicationResult);
        }
    }
}