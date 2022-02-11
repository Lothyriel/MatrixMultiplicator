using Domain.Connection;
using Domain.Exceptions;
using Domain.ExtensionMethods;
using Domain.Matrices;
using Domain.MatrixMultiplication;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class TDD
    {
        public Matrix MatrixA { get; }
        public Matrix MatrixB { get; }
        public LocalMultiplicator Multiplicator { get; }

        public static void MockFunc(int x, int y) { return; }
        public static void MockFunc(TimeSpan t) { return; }
        public static void MockFunc() { return; }

        public TDD()
        {
            MatrixA = GetMatrixA();

            MatrixB = GetMatrixB();
            Multiplicator = new LocalMultiplicator(MatrixA, MatrixB)
            {
                ResultHandler = MockFunc,
                ExitHandler = MockFunc
            };
        }

        [Test]
        public void ShouldMultiplyMatricesSingleThread()
        {
            var expectedMatrixResult = GetResultMatrix();

            Multiplicator.MultiplySingleThreaded();

            var resultMatrix = Multiplicator.Result!;

            resultMatrix.X.Should().Be(MatrixA.X);
            resultMatrix.Y.Should().Be(MatrixB.Y);

            for (int i = 0; i < resultMatrix.X; i++)
            {
                resultMatrix.InnerMatrix[i].Should().Equal(expectedMatrixResult.InnerMatrix[i]);
            }
        }

        [Test]
        public void ShouldMultiplyMatricesMultiThread()
        {
            var expectedMatrixResult = GetResultMatrix();

            Multiplicator.MultiplyMultiThreaded();

            var resultMatrix = Multiplicator.Result!;
            resultMatrix.X.Should().Be(MatrixA.X);
            resultMatrix!.Y.Should().Be(MatrixB.Y);

            for (int i = 0; i < resultMatrix.X; i++)
            {
                resultMatrix.InnerMatrix[i].Should().Equal(expectedMatrixResult.InnerMatrix[i]);
            }
        }

        [Test]
        public void ShouldFailCreatingMatrix()
        {
            var matrix = new List<List<double>>()
            {
                new List<double>(){ 18,26,-4},
                new List<double>(){ 30,13}
            };

            var transform = () => Matrix.Assert(matrix);
            transform.Should().Throw<InvalidMatrix>();
        }

        [Test]
        public void ShouldReturnValidColumn()
        {
            var matrix = new List<List<double>>()
            {
                new List<double>(){ 1},
                new List<double>(){ 2},
                new List<double>(){ 3},
            };

            var expected = new double[] { 1, 2, 3 };

            var col = new Matrix(matrix).GetColumn(0);
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

            var expectedResult1 = new MultiplicationResult(0, 0, 10);
            user.Send(expectedResult1);
            var result1 = TcpServer.ReceiveResult(data).Desserialize<MultiplicationResult>();

            var expectedResult2 = new MultiplicationResult(1, 1, 20);
            user.Send(expectedResult2);
            var result2 = TcpServer.ReceiveResult(data).Desserialize<MultiplicationResult>();

            result1.Should().Be(expectedResult1);
            result2.Should().Be(expectedResult2);
        }

        public static Matrix GetResultMatrix()
        {
            var matrix = new List<List<double>>()
            {
                new List<double>(){ 18,26,-4},
                new List<double>(){ 30,13,-11},
            };

            return new Matrix(matrix);
        }

        public static Matrix GetMatrixB()
        {
            var matrix = new List<List<double>>()
            {
                new List<double>(){ 6,4,-2},
                new List<double>(){ 0,7,1},
            };

            return new Matrix(matrix);
        }

        public static Matrix GetMatrixA()
        {
            var matrix = new List<List<double>>()
            {
                new List<double>(){ 3,2},
                new List<double>(){ 5,-1},
            };

            return new Matrix(matrix);
        }
    }
}