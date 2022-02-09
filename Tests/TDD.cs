using Domain.Connection;
using Domain.Exceptions;
using Domain.ExtensionMethods;
using Domain.Matrices;
using Domain.MatrixMultiplication;
using Domain.MatrixOperations;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class TDD
    {
        [Test]
        public void ShouldMultiplyMatricesSingleThread()
        {
            var matrixA = GetMatrixA();

            var matrixB = GetMatrixB();

            var expectedMatrixResult = GetResultMatrix();

            var resultMatrix = new LocalMultiplicator(matrixA, matrixB).MultiplySingleThreaded();

            resultMatrix.X.Should().Be(matrixA.X);
            resultMatrix.Y.Should().Be(matrixB.Y);

            for (int i = 0; i < resultMatrix.X; i++)
            {
                resultMatrix.InnerMatrix[i].Should().Equal(expectedMatrixResult.InnerMatrix[i]);
            }
        }

        [Test]
        public void ShouldMultiplyMatricesMultiThread()
        {
            var matrixA = GetMatrixA();

            var matrixB = GetMatrixB();

            var expectedMatrixResult = GetResultMatrix();

            var resultMatrix = new LocalMultiplicator(matrixA, matrixB).MultiplyMultiThreaded();

            resultMatrix.X.Should().Be(matrixA.X);
            resultMatrix.Y.Should().Be(matrixB.Y);

            for (int i = 0; i < resultMatrix.X; i++)
            {
                resultMatrix.InnerMatrix[i].Should().Equal(expectedMatrixResult.InnerMatrix[i]);
            }
        }

        [Test]
        public void ShouldFailCreatingMatrix()
        {
            var matrixList = new List<List<double>>()
            {
                new List<double>() { 18,26,-4},
                new List<double>() { 30,13},
            };

            var transform = () => MatrixReader.Assert(matrixList);
            transform.Should().Throw<InvalidMatrix>();
        }

        [Test]
        public void ShouldReturnValidColumn()
        {
            var matrix = new IncompleteMatrix();
            matrix[0][0] = 1;
            matrix[1][0] = 2;
            matrix[2][0] = 3;

            var expected = new double[] { 1, 2, 3 };

            var col = matrix.GetColumn(0).Denullify();
            col.Should().Equal(expected);
        }

        [Test]
        public void ShouldCommunicateThroughSockets()
        {
            var ip = "127.0.0.1";
            var port = 25565;

            var server = new TcpServer(ip, port);
            var user = new TcpUser(ip, port);

            var data = server.ReceiveConnection();
            user.Send(8);
            var eight = TcpServer.ReceiveResult(data);

            user.Send(50);
            var fifty = TcpServer.ReceiveResult(data);

            fifty.Should().Be("50");
            eight.Should().Be("8");
        }

        public static Matrix GetResultMatrix()
        {
            var expectedmatrixListResult = new List<List<double>>()
            {
                new List<double>() { 18,26,-4},
                new List<double>() { 30,13,-11},
            };
            return new Matrix(expectedmatrixListResult);
        }

        public static Matrix GetMatrixB()
        {
            var matrixListB = new List<List<double>>()
            {
                new List<double>() { 6,4,-2},
                new List<double>() { 0,7,1},
            };
            return new Matrix(matrixListB);
        }

        public static Matrix GetMatrixA()
        {
            var matrixListA = new List<List<double>>()
            {
                new List<double>() { 3,2},
                new List<double>() { 5,-1},
            };
            return new Matrix(matrixListA);
        }
    }
}