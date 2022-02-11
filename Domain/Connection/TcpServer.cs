using Domain.ExtensionMethods;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Domain.Connection
{
    public class TcpServer
    {
        private const int ReceiveBufferSize = 1024;

        public TcpServer(string ip, int port)
        {
            Listener = new TcpListener(IPAddress.Parse(ip), port);
            Listener.Start();
        }

        public TcpListener Listener { get; }

        public static void SendData(object data, ClientData clientData)
        {
            var json = JsonConvert.SerializeObject(data);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            var stream = clientData.Stream;
            stream.Write(bytes, 0, bytes.Length);
        }
        public ClientData ReceiveConnection()
        {
            var client = Listener.AcceptTcpClient();

            var ns = client.GetStream();
            return new ClientData(ns, client);
        }
        public static string ReceiveResult(ClientData data) 
        {
            byte[] buffer = new byte[ReceiveBufferSize];

            int bytesRead = data.Stream.Read(buffer, 0, ReceiveBufferSize);
            return Encoding.UTF8.GetString(buffer[..bytesRead]);
        }

        public void Dispose()
        {
            Listener.Stop();
        }
    }
}