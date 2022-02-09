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
                resultMatrix.InnerMatrix.InnerMatrix[i].InnerArray.Should().Equal(expectedMatrixResult.InnerMatrix.InnerMatrix[i].InnerArray);
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
                resultMatrix.InnerMatrix.InnerMatrix[i].InnerArray.Should().Equal(expectedMatrixResult.InnerMatrix.InnerMatrix[i].InnerArray);
            }
        }

        [Test]
        public void ShouldFailCreatingMatrix()
        {
            var matrix = new IncompleteMatrix();
            matrix[0][0] = 18;
            matrix[0][1] = 26;
            matrix[0][2] = -4;

            matrix[1][0] = 30;
            matrix[1][1] = 13;

            var transform = () => MatrixReader.Assert(matrix);
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

            var expectedResult1 = new MultiplicationResult(0, 0, 10);
            user.Send(expectedResult1);
            var result1 = TcpServer.ReceiveResult(data);

            var expectedResult2 = new MultiplicationResult(1, 1, 20);
            user.Send(expectedResult2);
            var result2 = TcpServer.ReceiveResult(data);

            result1.Should().Be(expectedResult1);
            result2.Should().Be(expectedResult2);
        }

        public static Matrix GetResultMatrix()
        {
            var matrix = new IncompleteMatrix();
            matrix[0][0] = 18;
            matrix[0][1] = 26;
            matrix[0][2] = -4;

            matrix[1][0] = 30;
            matrix[1][1] = 13;
            matrix[1][2] = -11;

            return new Matrix(matrix);
        }

        public static Matrix GetMatrixB()
        {
            var matrix = new IncompleteMatrix();
            matrix[0][0] = 6;
            matrix[0][1] = 4;
            matrix[0][2] = -2;

            matrix[1][0] = 0;
            matrix[1][1] = 7;
            matrix[1][2] = 1;

            return new Matrix(matrix);
        }

        public static Matrix GetMatrixA()
        {
            var matrix = new IncompleteMatrix();
            matrix[0][0] = 3;
            matrix[0][1] = 2;

            matrix[1][0] = 5;
            matrix[1][1] = -1;

            return new Matrix(matrix);
        }
    }
}