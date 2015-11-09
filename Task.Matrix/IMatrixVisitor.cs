using Task.Matrix.Models;

namespace Task.Matrix {
    public interface IMatrixVisitor<T> {
        void Visit(SquareMatrix<T> matrix, Matrix<T> addMatrix);
        void Visit(SimmetricMatrix<T> matrix, Matrix<T> addMatrix);
        void Visit(DiagonalMatrix<T> matrix, Matrix<T> addMatrix);
    }
}
