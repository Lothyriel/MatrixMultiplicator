using Domain;
using Domain.MatrixMultiplication;
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
            var distributedMultiplicator = new DistributedMultiplicatorClient();
            distributedMultiplicator.MatrixA.DoubleMatrix.Add(new List<double> { 3, 2 });
            distributedMultiplicator.MatrixB.DoubleMatrix.Add(new List<double> { 6 });
            distributedMultiplicator.MatrixB.DoubleMatrix.Add(new List<double> { 0 });

            var expectedResult = 18;

            var lineResult = distributedMultiplicator.MultiplyLineByColumn(null, 0, null, 0);

            lineResult.Should().Be(expectedResult);
        }

        [Test]
        public void ShouldMultiplyLineByColumn()
        {
            var line = new List<double> { 3, 2 };
            var column = new List<double> { 6, 0 };

            var expectedResult = 18;

            var distributedMultiplicator = new DistributedMultiplicatorClient();

            var lineResult = distributedMultiplicator.MultiplyLineByColumn(line, 0, column, 0);

            lineResult.Should().Be(expectedResult);
        }
    }
}