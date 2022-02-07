using Newtonsoft.Json;
using System.Text;

namespace Domain.Connection
{
    public class MatrixConnection
    {
        private UdpConnection Connection { get; }

        public MatrixConnection(string ip, int port)
        {
            Connection = new(ip, port);
        }

        public void Send(object toSend, string ip)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(toSend));
            Connection.Send(bytes);
        }

        public string Receive()
        {
            var bytes = Connection.Receive();
            return Encoding.UTF8.GetString(bytes);
        }
    }
    public record MultiplicationRequest(double[]? Line, int Xm, double[]? Column, int Ym);
    public record MultiplicationResult(int Xm, int Ym, double Result);
    public record ConnectionInfo(string IP, int Port);
}