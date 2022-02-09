using Domain.Connection;
using Domain.ExtensionMethods;
using Domain.MatrixMultiplication;

namespace Domain.Application
{
    public class Slave
    {
        public Slave(string ip, int port)
        {
            MatrixMultiplicator = new();
            MatrixConnection = new(ip, port);
        }

        public ClientMatrixConnection MatrixConnection { get; }
        public DistributedMultiplicatorSlave MatrixMultiplicator { get; set; }

        public void Start()
        {
            StartEvaluating(SendResultAsync);
        }

        public void SendConnectionAttempt()
        {
            //MatrixConnection.Send(1);
        }

        public double EvaluateRequest(MultiplicationData request)
        {
            return MatrixMultiplicator.MultiplyLineByColumn(request.Line, request.Xm, request.Column, request.Ym);
        }

        public void SendResult(MultiplicationData request)
        {
            double result = EvaluateRequest(request);
            MatrixConnection.Send(new MultiplicationResult(request.Xm, request.Ym, result));
        }

        public void SendResultAsync(MultiplicationData request)
        {
            Task.Run(() => SendResult(request));
        }
        private void StartEvaluating(Action<MultiplicationData> handler)
        {
            while (true)
            {
                var received = MatrixConnection.Receive();
                if (received == "0")
                    break;

                if (received.StartsWith("{\"L"))
                    handler(received.Desserialize<MultiplicationData>());
            }
        }
    }
}