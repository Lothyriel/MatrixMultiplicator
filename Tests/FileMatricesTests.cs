using Domain;
using Domain.Matrices;
using Domain.MatrixMultiplication;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Tests
{
    public class FileMatricesTests
    {
        public const string PathMatrixA = "..\\..\\..\\..\\matA.txt";
        public const string PathMatrixB = "..\\..\\..\\..\\matB.txt";

        [Test]
        [TestCase(PathMatrixA)]
        [TestCase(PathMatrixB)]
        public void ShouldReadMatrix(string path)
        {
            var reader = new MatrixReader(path);
            var matrix = reader.Build();

            matrix.X.Should().Be(4096);
            matrix.Y.Should().Be(4096);
        }

        [Test]
        public void ShouldMultiplyFileMatricesSingleThreaded()
        {
            var matrixA = new MatrixReader(PathMatrixA).Build();
            var matrixB = new MatrixReader(PathMatrixB).Build();

            var multiplicator = new LocalMultiplicator(matrixA, matrixB);

            var resultMatrix = multiplicator.MultiplySingleThreaded();
            resultMatrix.X.Should().Be(matrixA.X);
            resultMatrix.Y.Should().Be(matrixB.Y);
        }

        [Test]
        public void ShouldMultiplyFileMatricesMultiThreaded()
        {
            var matrixA = new MatrixReader(PathMatrixA).Build();
            var matrixB = new MatrixReader(PathMatrixB).Build();

            var multiplicator = new LocalMultiplicator(matrixA, matrixB);

            var resultMatrix = multiplicator.MultiplyMultiThreaded();
            resultMatrix.X.Should().Be(matrixA.X);
            resultMatrix.Y.Should().Be(matrixB.Y);
        }

        [Test]
        [TestCase(PathMatrixA, "c06ab7d1d97602cdca927504fcf9b997")]
        [TestCase(PathMatrixB, "3428d1ca214241a897c4370973357df9")]
        public void ShouldVerifyMD5Hash(string path, string expectedHash)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(path);
            var bytes = md5.ComputeHash(stream);
            var hash = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();

            hash.Should().Be(expectedHash);
        }
    }
}