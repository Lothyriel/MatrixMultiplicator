namespace Domain.Connection
{
    public class ServerMatrixConnection
    {
        public TcpServer Connection { get; }

        public ServerMatrixConnection(string ip, int port)
        {
            Connection = new(ip, port);
        }

        public static void SendRequest(MultiplicationRequest data, ClientData clientData)
        {
            TcpServer.SendMultiplicationRequest(data, clientData);
        }

        public ClientData ReceiveConnection() 
        {
            return Connection.ReceiveConnection();   
        }

        public static MultiplicationResult ReceiveResult(ClientData client) 
        {
            return TcpServer.ReceiveResult(client);
        }
    }
}