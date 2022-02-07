using Domain.Application;
using Domain.Connection;
using Domain.ExtensionMethods;
using Domain.MatrixMultiplicators;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Text;

namespace Tests
{
    public class ConnectionTests
    {
        [Test]
        public void ShouldDesserializeMultiplicationRequest()
        {
            var toSend = new MultiplicationRequest(null, 0, null, 0);

            var serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(toSend));

            var data = Encoding.UTF8.GetString(serialized);

            data.Desserialize<MultiplicationRequest>().Should().Be(toSend);
        }
        [Test]
        public void ShouldMultiplyMatricesUsingLocalSockets()
        {
            var matrixA = TDD.GetMatrixA();
            var matrixB = TDD.GetMatrixB();

            var ip = "192.168.10.1";
            var master = new Master(ip, matrixA, matrixB);
            master.IPs.Add(ip);

            master.StartReceivingLoop();
            master.Start();

            var slave = new Slave(ip);
            slave.Start();
        }
    }
}