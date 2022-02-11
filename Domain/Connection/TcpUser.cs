using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace Domain.Connection
{
    public class TcpUser
    {
        private const int BufferSize = 65536;

        public TcpUser(string ip, int port)
        {
            Client = new TcpClient(ip, port);
            Stream = Client.GetStream();
        }
        public TcpClient Client { get; }
        public NetworkStream Stream { get; }

        public string Receive()
        {
            byte[] bytes = new byte[BufferSize];
            int bytesRead = Stream.Read(bytes, 0, bytes.Length);

            return Encoding.UTF8.GetString(bytes, 0, bytesRead);
        }

        public void Send(MultiplicationResult result)
        {
            var json = JsonConvert.SerializeObject(result);

            byte[] bytesToSend = Encoding.UTF8.GetBytes(json);

            Stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public void Dispose()
        {
            Client.Close();
        }
    }
}