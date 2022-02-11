using Domain.Connection;
using Domain.MatrixMultiplication;
using System.Diagnostics;

namespace Domain.Application
{
    public class Slave
    {
        public Slave(string ip, int port)
        {
            MatrixConnection = new(ip, port);
            MatrixMultiplicator = new();
            Handler = new();
        }

        public ClientMatrixConnection MatrixConnection { get; }
        public DistributedMultiplicatorSlave MatrixMultiplicator { get; set; }

        public UnstickHandler<MultiplicationRequest> Handler { get; }

        public Action? ExitHandler { get; set; }

        public void Start()
        {
            Task.Run(() => StartEvaluating(Handler));

            void Handler(string request)
            {
                HandleRequest(request);
            }
        }
        public void StartMultiThread()
        {
            Task.Run(() => StartEvaluating(AsyncHandler));

            void AsyncHandler(string request)
            {
                Task.Run(() => HandleRequest(request));
            }
        }

        public void HandleRequest(MultiplicationRequest request)
        {
            Debug.WriteLine($"Resolvendo a request {request}");
            double result = MatrixMultiplicator.MultiplyLineByColumn(request.Line, request.Xm, request.Column, request.Ym);
            Debug.WriteLine($"Enviando o resultado {result} {request.Xm} {request.Ym}");
            MatrixConnection.Send(new MultiplicationResult(request.Xm, request.Ym, result));
        }

        private void StartEvaluating(Action<string> requestHandler)
        {
            while (true)
            {
                var received = MatrixConnection.Receive();
                requestHandler(received);
            }
        }
        private void HandleRequest(string received)
        {
            if (received == "0")
                ExitHandler!();

            Handler.Add(received);
            HandleRequest(Handler.Next().Result);
        }
    }
}