using Domain.Connection;
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

        public void Start() 
        {
            StartReceivingConnections();
            EndHandler();
        }
        public void StartSendingRequests()
        {
            Multiplicator.SendMultiplicationRequests();
        }

        private void StartReceivingConnections()
        {
            Task.Run(ReceivingLoop);

            void ReceivingLoop()
            {
                while (true)
                {
                    var clientData = MatrixConnection.ReceiveConnection();
                    Multiplicator.AddClientData(clientData);
                    HandleClientResults(clientData);
                }
            }
        }
        private void HandleClientResults(ClientData clientData)
        {
            Task.Run(() => HandleLoop(clientData));

            void HandleLoop(ClientData clientData) 
            {
                while (clientData is not null)
                {
                    var result = ServerMatrixConnection.ReceiveResult(clientData);
                    Multiplicator.UpdateResult(result);
                }
            }
        }
        private void EndHandler()
        {
            Task.Run(CheckLoop);

            void CheckLoop()
            {
                while (!Multiplicator.Ended())
                {
                    Thread.Sleep(3000);
                }
                Multiplicator.CloseConnections();
            }
        }
    }
}