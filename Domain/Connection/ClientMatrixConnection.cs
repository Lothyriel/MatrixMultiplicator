using Domain.ExtensionMethods;
using Newtonsoft.Json;
using System.Text;

namespace Domain.Connection
{
    public class ClientMatrixConnection
    {
        public ClientMatrixConnection(string ip, int port)
        {
            Connection = new(ip, port);
        }
        public TcpUser Connection { get; }

        public string Receive() 
        {
            return Connection.Receive();
        }

        public void Send(MultiplicationResult result)
        {
            Connection.Send(result);
        }
    }
}