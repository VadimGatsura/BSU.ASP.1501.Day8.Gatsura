using System.Collections.Generic;
using NUnit.Framework;
using Task.Matrix.Models;

namespace Task.Matrix.NUnitTests {
    [TestFixture]
    public class ComputeMatrixSumVisitorTest {

        private IEnumerable<TestCaseData> TestDatas {
            get {
                yield return new TestCaseData(
                    new SquareMatrix<int>(new[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } }),
                    new DiagonalMatrix<int>(new[,] { { 1, 0, 0 }, { 0, 2, 0 }, { 0, 0, 3 } }),
                    new SquareMatrix<int>(new[,] { { 1, 1, 2 }, { 3, 6, 5 }, { 6, 7, 11 } }));
            }
        }

        [TestCaseSource(nameof(TestDatas))]
        public void MatrixsSum_Test(SquareMatrix<int> firstMatrix, SquareMatrix<int> secondMatrix, SquareMatrix<int> resultMatrix ) {
            SquareMatrix<int> result = firstMatrix.Add(secondMatrix);

            Assert.AreEqual(resultMatrix.Equals(result), true);
        }

        private IEnumerable<TestCaseData> TestEqualsDatas {
            get {
                yield return new TestCaseData(
                    new SquareMatrix<int>(new[,] { { 1, 1, 2 }, { 3, 6, 5 }, { 6, 7, 11 } }),
                    new SquareMatrix<int>(new[,] { { 1, 1, 2 }, { 3, 6, 5 }, { 6, 7, 11 } }),
                    true);
            }
        }
        [TestCaseSource(nameof(TestEqualsDatas))]
        public void Matrix_Equals_Test(SquareMatrix<int> firstMatrix, SquareMatrix<int> secondMatrix, bool result) {
            Assert.AreEqual(firstMatrix.Equals(secondMatrix), result);    
        }
    }
}
