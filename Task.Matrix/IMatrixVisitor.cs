using Task.Matrix.Models;

namespace Task.Matrix {
    public interface IMatrixVisitor<T> {
        void Visit(SquareMatrix<T> matrix, SquareMatrix<T> addMatrix);
        void Visit(SymmetricMatrix<T> matrix, SquareMatrix<T> addMatrix);
        void Visit(DiagonalMatrix<T> matrix, SquareMatrix<T> addMatrix);
    }
}
