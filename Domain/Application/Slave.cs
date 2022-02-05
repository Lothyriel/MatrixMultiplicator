using Domain.Connection;
using Domain.ExtensionMethods;
using Domain.MatrixMultiplication;

namespace Domain.Application
{
    public class Slave
    {
        public Slave(string ip)
        {
            MatrixConnection = new(ip, 25565);
            MatrixMultiplicator = new();
            Ip = ip;
        }

        public MatrixConnection MatrixConnection { get; }
        public DistributedMultiplicatorSlave MatrixMultiplicator { get; }
        public string Ip { get; }

        public void Start()
        {
            StartEvaluating(Evaluate);
        }
        public void StartAsync()
        {
            StartEvaluating(EvaluateAsync);
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

        public void Evaluate(MultiplicationRequest request)
        {
            var result = MatrixMultiplicator.MultiplyLineByColumn(request.Line, request.Xm, request.Column, request.Ym);
            MatrixConnection.Send(new MultiplicationResult(request.Xm, request.Ym, result), Ip);
        }

        public void EvaluateAsync(MultiplicationRequest request)
        {
            Task.Run(() => Evaluate(request));
        }
    }
}