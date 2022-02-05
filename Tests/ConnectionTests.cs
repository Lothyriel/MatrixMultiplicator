using Domain.Connection;
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

            ClientConnection.Desserialize(data).Should().Be(toSend);
        }
    }
}