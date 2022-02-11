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
            TcpServer.SendData(data, clientData);
        }
        public static void TerminateConnection(ClientData clientData) 
        {
            TcpServer.SendData("0", clientData);
        }

        public ClientData ReceiveConnection() 
        {
            return Connection.ReceiveConnection();   
        }

        public static string ReceiveResult(ClientData client) 
        {
            return TcpServer.ReceiveResult(client);
        }
    }
}