using Domain.Application;
using Domain.Connection;
using Domain.ExtensionMethods;
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
        public void ShouldMultiplyLineByColumnThroughSocket()
        {
            var matrixA = TDD.GetMatrixA();
            var matrixB = TDD.GetMatrixB();

            var port = 25565;
            var ip = "127.0.0.1";

            var master = new Master(new(matrixA, matrixB), ip, port);

            var slave = new Slave(ip, port)
            {
                ExitHandler = TDD.MockFunc
            };

            master.Multiplicator.AddClientData(master.MatrixConnection.ReceiveConnection());
            var clientData = master.Multiplicator.GetNextClientData();
            master.Multiplicator.DistributedMultiplication(0, 0, clientData);

            var recevived = slave.MatrixConnection.Receive();
            slave.HandleRequest(recevived.Desserialize<MultiplicationRequest>());

            var result = ServerMatrixConnection.ReceiveResult(clientData).Desserialize<MultiplicationResult>();

            master.Multiplicator.UpdateResult(result);

            result.Result.Should().Be(18);
            master.Multiplicator.Result[0, 0].Should().Be(18);
        }
    }
}