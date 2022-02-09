using System.Net.Sockets;

namespace Domain.Connection
{
    public record ClientData(NetworkStream Stream, TcpClient Client);
    public record MultiplicationRequest(ClientData ClientData, string Data);
    public record MultiplicationData(List<double>? Line, int Xm, List<double>? Column, int Ym);
    public record MultiplicationResult(int Xm, int Ym, double Result);
}
