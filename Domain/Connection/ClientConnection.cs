using Domain.Exceptions;
using Domain.MatrixMultiplication;
using Newtonsoft.Json;

namespace Domain.Connection
{
    public class ClientConnection
    {
        public ClientConnection(string ip)
        {
            MatrixConnection = new(ip, 25565);
            MatrixMultiplicator = new();
        }

        public MatrixConnection MatrixConnection { get; }
        public DistributedMultiplicatorClient MatrixMultiplicator { get; }

        public void StartEvaluating(Action<MultiplicationRequest> handler)
        {
            while (true)
            {
                var received = MatrixConnection.Receive();
                if (received == "0")
                    break;

                if (received.StartsWith("{"))
                    handler(Desserialize(received));
            }
        }

        public void Evaluate(MultiplicationRequest request, string ip)
        {
            var result = MatrixMultiplicator.MultiplyLineByColumn(request.Line, request.Xm, request.Column, request.Ym);
            MatrixConnection.Send(new MultiplicationResult(request.Xm, request.Ym, result), ip);
        }

        public void EvaluateAsync(MultiplicationRequest request, string ip)
        {
            Task.Run(() => Evaluate(request, ip));
        }

        public static MultiplicationRequest Desserialize(string received)
        {
            return JsonConvert.DeserializeObject<MultiplicationRequest>(received) ?? throw new DesserializeException(received);
        }
    }
}