﻿using Domain.MatrixMultiplication;
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

            distributedMultiplicator.MatrixALines[0] = new List<double>() { 3, 2 };

            distributedMultiplicator.MatrixBColumns[0] = new List<double>() { 6, 0 };

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

            var distributedMultiplicator = new DistributedMultiplicatorSlave();

            var lineResult = distributedMultiplicator.MultiplyLineByColumn(line, 0, column, 0);

            lineResult.Should().Be(expectedResult);
        }
    }
}