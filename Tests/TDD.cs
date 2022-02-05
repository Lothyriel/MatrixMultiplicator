using Domain.Exceptions;
using Domain.Matrices;
using Domain.MatrixMultiplication;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class TDD
    {
        [Test]
        public void ShouldMultiplyMatrices()
        {
            var matrixA = GetMatrixA();

            var matrixB = GetMatrixB();

            var expectedMatrixResult = GetResultMatrix();

            var resultMatrix = new LocalMultiplicator(matrixA, matrixB).MultiplySingleThreaded();

            resultMatrix.X.Should().Be(matrixA.X);
            resultMatrix.Y.Should().Be(matrixB.Y);

            for (int i = 0; i < resultMatrix.X; i++)
            {
                resultMatrix.DoubleMatrix[i].Should().Equal(expectedMatrixResult.DoubleMatrix[i]);
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

        public static Matrix GetResultMatrix()
        {
            var expectedmatrixListResult = new List<List<double>>()
            {
                new List<double>() { 18,26,-4},
                new List<double>() { 30,13,-11},
            };
            var expectedMatrixResult = new Matrix(expectedmatrixListResult);
            return expectedMatrixResult;
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