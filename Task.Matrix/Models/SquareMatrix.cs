using System;
using System.Collections.Generic;

namespace Task.Matrix.Models {
    public class SquareMatrix<T> : Matrix<T> {
        private readonly T[,] m_Array;

        #region Constructors

        protected SquareMatrix() { } 

        public SquareMatrix(int size) {
            if(size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));
            Size = size;
            m_Array = new T[Size, Size];
            for(int i = 0; i < Size; i++) {
                for(int j = 0; j < Size; j++)
                    m_Array[i, j] = default(T);
            }

        }

        public SquareMatrix(T[,] inArray) {
            if(inArray == null)
                throw new ArgumentNullException(nameof(inArray));
            if(inArray.GetLength(0) != inArray.GetLength(1) && inArray.GetLength(0) > 0)
                throw new ArgumentException($"Argument {nameof(inArray)} doesn't square array");

            Size = inArray.GetLength(0);
            m_Array = new T[Size, Size];
            for (int i = 0; i < Size; i++) {
                for (int j = 0; j < Size; j++)
                    m_Array[i, j] = inArray[i, j];
            }
        }

        #endregion

        #region Public Methods

        public IEnumerable<T> GetMatrix() {
            for(int i = 0; i < Size; i++)
                for(int j = 0; j < Size; j++)
                    yield return this[i, j];
        }

        
        public override int GetHashCode() => m_Array?.GetHashCode() ?? 0;
        #endregion

        #region Protected Methods

        protected override T GetValue(int i, int j) => m_Array[i, j];

        protected override void SetValue(int i, int j, T value) {
            m_Array[i, j] = value;
        }
        #endregion
    }
}
