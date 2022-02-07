using Domain.Connection;
using Domain.ExtensionMethods;
using Domain.MatrixMultiplication;

namespace Domain.Application
{
    public class Slave
    {
        public Slave(string ip)
        {
            MatrixMultiplicator = new();
            MatrixConnection = new(ip, 25565);
            Ip = ip;
        }

        public MatrixConnection MatrixConnection { get; }
        public DistributedMultiplicatorSlave MatrixMultiplicator { get; set; }
        public string Ip { get; }

        public void Start()
        {
            StartEvaluating(SendResult);
        }
        public void StartAsync()
        {
            StartEvaluating(SendResultAsync);
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

        public double EvaluateRequest(MultiplicationRequest request)
        {
            return MatrixMultiplicator.MultiplyLineByColumn(request.Line, request.Xm, request.Column, request.Ym);
        }

        public void SendResult(MultiplicationRequest request)
        {
            double result = EvaluateRequest(request);
            MatrixConnection.Send(new MultiplicationResult(request.Xm, request.Ym, result), Ip);
        }

        public void SendResultAsync(MultiplicationRequest request)
        {
            Task.Run(() => SendResult(request));
        }
    }
}