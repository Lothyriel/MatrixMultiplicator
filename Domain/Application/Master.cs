using Domain.Connection;
using Domain.Matrices;
using Domain.MatrixMultiplicators;

namespace Domain.Application
{
    public class Master
    {
        public Master(DistributedMultiplicatorMaster multiplicator, string ip, int port)
        {
            MatrixConnection = new(ip, port);
            Multiplicator = multiplicator;
        }

        public ServerMatrixConnection MatrixConnection { get; }
        public DistributedMultiplicatorMaster Multiplicator { get; set; }

        public void StartRequests()
        {
            Multiplicator.SendMultiplicationRequests();
        }
        public void StartReceiving()
        {
            Task.Run(Loop);

            void Loop()
            {
                while (true)
                {
                    Multiplicator.ClientsData.Add(MatrixConnection.Receive());
                }
            }
        }
        public void EndHandler()
        {
            Task.Run(CheckLoop);

            void CheckLoop()
            {
                while (!Multiplicator.Ended())
                {
                    Thread.Sleep(3000);
                }
                CloseConnections();
            }
        }

        private void CloseConnections()
        {
            foreach (var clientData in Multiplicator.ClientsData)
            {
                Task.Run(() => EndConnection(clientData));
            }
            void EndConnection(ClientData clientData)
            {
                clientData.Client.Close();
                clientData.Stream.Close();
            }
        }
        private void HandleResult(MultiplicationResult multiplicationResult)
        {
            Multiplicator.UpdateResult(multiplicationResult);
        }
    }
}