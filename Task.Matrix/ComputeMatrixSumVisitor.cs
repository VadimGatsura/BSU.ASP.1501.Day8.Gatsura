using System;
using System.Linq.Expressions;
using Task.Matrix.Models;

namespace Task.Matrix {
    internal class ComputeMatrixSumVisitor<T>: IMatrixVisitor<T> {
        public T Sum { get; private set; }
        public void Visit(SquareMatrix<T> matrix, Matrix<T> addMatrix) {
            
        }

        public void Visit(SimmetricMatrix<T> matrix, Matrix<T> addMatrix) {
            throw new NotImplementedException();
        }

        public void Visit(DiagonalMatrix<T> matrix, Matrix<T> addMatrix) {
            throw new NotImplementedException();
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
