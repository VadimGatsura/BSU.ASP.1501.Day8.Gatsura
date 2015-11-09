using Task.Matrix.Models;

namespace Task.Matrix {
    public static class MatrixExtensions {
        public static SquareMatrix<T> Add<T>(this SquareMatrix<T> matrix, SquareMatrix<T> addMatrix) {
            var visitor = new ComputeMatrixSumVisitor<T>();
            matrix.Accept(visitor, addMatrix);
            return visitor.Result;
        }
    }
}
