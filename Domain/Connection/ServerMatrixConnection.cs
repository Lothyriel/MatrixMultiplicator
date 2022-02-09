namespace Domain.Connection
{
    public class ServerMatrixConnection
    {
        public TcpServer Connection { get; }

        public ServerMatrixConnection(string ip, int port)
        {
            Connection = new(ip, port);
        }

        public static void SendRequest(MultiplicationData data, ClientData clientData)
        {
            TcpServer.SendReply(data, clientData);
        }
        public ClientData Receive() 
        {
            return Connection.ReceiveConnection();   
        }
    }
}