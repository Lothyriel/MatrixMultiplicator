using Domain.Application;
using Domain.Connection;
using Domain.ExtensionMethods;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ConnectionTests
    {
        [Test]
        public void ShouldDesserializeMultiplicationRequest()
        {
            var toSend = new MultiplicationData(null, 0, null, 0);

            var serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(toSend));

            var data = Encoding.UTF8.GetString(serialized);

            data.Desserialize<MultiplicationData>().Should().Be(toSend);
        }
        [Test]
        public void ShouldConnect()
        {
            var matrixA = TDD.GetMatrixA();
            var matrixB = TDD.GetMatrixB();

            var port = 25565;
            var ip = "127.0.0.1";

            var master = new Master(new(matrixA, matrixB), ip, port);

            var slave = new Slave(ip, port);

            master.StartReceiving();
            slave.SendConnectionAttempt();
            master.StartRequests();
        }
    }
}