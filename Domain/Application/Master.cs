using Domain.Connection;
using Domain.Exceptions;
using Domain.MatrixMultiplicators;
using System.Diagnostics;

namespace Domain.Application
{
    public class Master
    {
        public Master(DistributedMultiplicatorMaster multiplicator, string ip, int port)
        {
            MatrixConnection = new(ip, port);
            Multiplicator = multiplicator;
            Handler = new();
        }

        public ServerMatrixConnection MatrixConnection { get; }
        public DistributedMultiplicatorMaster Multiplicator { get; set; }

        public UnstickHandler<MultiplicationResult> Handler { get; set; }

        public void Start(Action<int, int> resultHandler) 
        {
            StartReceivingConnections(resultHandler);
            StartEndHandlerLoop();
        }
        public void StartSendingRequests()
        {
            if (!Multiplicator.ClientsData.Any())
                throw new NoClientsConnected();

            Multiplicator.SendMultiplicationRequests();
        }

        private void StartReceivingConnections(Action<int, int> resultHandler)
        {
            Task.Run(ReceivingLoop);

            void ReceivingLoop()
            {
                while (true)
                {
                    var clientData = MatrixConnection.ReceiveConnection();
                    Multiplicator.AddClientData(clientData);
                    HandleClientResults(clientData, resultHandler);
                }
            }
        }
        private void HandleClientResults(ClientData clientData, Action<int, int> resultHandler)
        {
            Task.Run(() => HandleLoop(clientData));

            void HandleLoop(ClientData clientData) 
            {
                while (clientData.Client.Connected)
                {
                    var received = ServerMatrixConnection.ReceiveResult(clientData);
                    Handler.Add(received);
                    var result =  Handler.Next().Result;

                    Debug.WriteLine($"Recebendo o resultado {result} {result.Xm} {result.Ym}");

                    resultHandler(result.Xm, result.Ym);
                    Multiplicator.UpdateResult(result);
                    Debug.WriteLine($"Atualizado o resultado {result} {result.Xm} {result.Ym}");
                }
            }
        }
        private void StartEndHandlerLoop()
        {
            Task.Run(CheckLoop);

            async void CheckLoop()
            {
                while (!Multiplicator.Ended())
                {
                    await Task.Delay(3000);
                }
                Multiplicator.CloseConnections();
            }
        }
    }
}