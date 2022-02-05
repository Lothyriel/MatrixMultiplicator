using Domain.MatrixMultiplicators;

namespace Domain.Connection
{
    public class ServerConnection
    {
        public ServerConnection(string ip, DistributedMultiplicatorServer multiplicator)
        {
            MatrixConnection = new(ip, 25565);
            Multiplicator = multiplicator;
        }

        public MatrixConnection MatrixConnection { get; }

        public DistributedMultiplicatorServer Multiplicator { get; set; }

        public void StartReceivingLoop()
        {
            while (true)
            {
                var received = MatrixConnection.Receive();
                if (received == "0")
                    break;

                if (received.StartsWith("{"))
                    HandleResult(received.Desserialize<MultiplicationResult>());
            }
        }

        private void HandleResult(MultiplicationResult multiplicationResult)
        {
            Multiplicator!.UpdateResult(multiplicationResult);
        }
    }
}