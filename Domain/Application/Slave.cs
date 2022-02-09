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

        public double EvaluateRequest(MultiplicationRequest request)
        {
            return MatrixMultiplicator.MultiplyLineByColumn(request.Line, request.Xm, request.Column, request.Ym);
        }

        public void SendResult(MultiplicationRequest request)
        {
            double result = EvaluateRequest(request);
            MatrixConnection.Send(new MultiplicationResult(request.Xm, request.Ym, result));
        }

        public void SendResultAsync(MultiplicationRequest request)
        {
            Task.Run(() => SendResult(request));
        }
        private void StartEvaluating(Action<MultiplicationRequest> handler)
        {
            while (true)
            {
                var received = MatrixConnection.Receive();
                if (received == "0")
                    break;

                if (received.StartsWith("{\"L"))
                    handler(received.Desserialize<MultiplicationRequest>());
            }
        }
    }
}