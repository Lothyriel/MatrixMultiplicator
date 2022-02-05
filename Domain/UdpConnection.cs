﻿using System.Net;
using System.Net.Sockets;

namespace Domain
{
    public class UdpConnection
    {
        public UdpConnection(string ipAddress, int port)
        {
            var multiCastIP = IPAddress.Parse(ipAddress);

            RemoteEndPoint = new IPEndPoint(multiCastIP, port);
            Client = new UdpClient();

            Client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            Client.ExclusiveAddressUse = false;
            Client.Client.Bind(new IPEndPoint(IPAddress.Any, port));
        }
        private IPEndPoint RemoteEndPoint { get; }
        private UdpClient Client { get; }

        public void Send(byte[] buffer)
        {
            Client.Send(buffer, buffer.Length, RemoteEndPoint);
        }
        public byte[] Receive()
        {
            var sender = new IPEndPoint(0, 0);
            return Client.Receive(ref sender);
        }
    }
}