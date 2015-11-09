using System;
using System.Linq.Expressions;
using Task.Matrix.Models;

namespace Task.Matrix {
    internal class ComputeMatrixSumVisitor<T>: IMatrixVisitor<T> {
        public SquareMatrix<T> Result { get; private set; }
        public void Visit(SquareMatrix<T> matrix, SquareMatrix<T> addMatrix) {
            if(matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (addMatrix == null)
                throw new ArgumentNullException(nameof(addMatrix));
            if (matrix.Size != addMatrix.Size)
                throw new ArgumentException("Matrix sizes doesn't match");
        }

        public void Visit(SymmetricMatrix<T> matrix, SquareMatrix<T> addMatrix) {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (addMatrix == null)
                throw new ArgumentNullException(nameof(addMatrix));
            if (matrix.Size != addMatrix.Size)
                throw new ArgumentException("Matrix sizes doesn't match");
        }

        public void Visit(DiagonalMatrix<T> matrix, SquareMatrix<T> addMatrix) {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (addMatrix == null)
                throw new ArgumentNullException(nameof(addMatrix));
            if (matrix.Size != addMatrix.Size)
                throw new ArgumentException("Matrix sizes doesn't match");

            try {
                Result = new SquareMatrix<T>(matrix.Size);
                for(int i = 0; i < matrix.Size; i++)
                    for(int j = 0; j < matrix.Size; j++)
                        Result[i,j] = AddHelper<T>.Add(matrix[i,j], addMatrix[i,j]);
            } catch(InvalidOperationException ex) {
                throw new NotSupportedException($"Add of two matrix for {typeof(T)} not suported", ex);
            }
        }

    }

    public static class AddHelper<T> {

        private static readonly Func<T, T, T> m_Add; 

        static AddHelper() {
            ParameterExpression paramA = Expression.Parameter(typeof(T), "lhs"),
                                paramB = Expression.Parameter(typeof(T), "rhs");

            BinaryExpression body = Expression.Add(paramA, paramB);
            m_Add = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();
        } 

        public static T Add(T lhs, T rhs) => m_Add(lhs, rhs);
    }
}
