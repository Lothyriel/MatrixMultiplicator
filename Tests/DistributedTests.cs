using Domain.MatrixMultiplication;
using Domain.MatrixOperations;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class DistributedTests
    {
        [Test]
        public void ShouldMultiplyLineByColumnNullValue()
        {
            var distributedMultiplicator = new DistributedMultiplicatorSlave();
            var matrixA = distributedMultiplicator.MatrixA;
            matrixA[0][0] = 3;
            matrixA[0][1] = 2;

            var matrixB = distributedMultiplicator.MatrixB;
            matrixB[0][0] = 6;
            matrixB[1][0] = 0;

            var expectedResult = 18;

            var lineResult = distributedMultiplicator.MultiplyLineByColumn(null, 0, null, 0);

            lineResult.Should().Be(expectedResult);
        }

        [Test]
        public void ShouldMultiplyLineByColumn()
        {
            var line = new double[] { 3, 2 };
            var column = new double[] { 6, 0 };

            var expectedResult = 18;

            var distributedMultiplicator = new DistributedMultiplicatorSlave();

            var lineResult = distributedMultiplicator.MultiplyLineByColumn(line, 0, column, 0);

            lineResult.Should().Be(expectedResult);
        }
    }
}