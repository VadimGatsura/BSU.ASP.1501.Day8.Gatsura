using System;

namespace Task.Matrix.Models {
    public class DiagonalMatrix<T> : SquareMatrix<T> {
        private readonly T[] m_Array;
        #region Constructors

        public DiagonalMatrix(int size) {
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));
            Size = size;
            m_Array = new T[Size];
            for (int i = 0; i < Size; i++) {
                    m_Array[i] = default(T);
            }
        }

        public DiagonalMatrix(T[,] inArray) {
            if (inArray.GetLength(0) != inArray.GetLength(1) && inArray.GetLength(0) > 0)
                throw new ArgumentException($"Argument {nameof(inArray)} doesn't square array");
            if (!CheckDiagonal(inArray))
                throw new ArgumentException($"{nameof(inArray)} doesn't symmetric matrix");
            Size = inArray.GetLength(0);
            m_Array = new T[Size];
            for(int i = 0; i < Size; i++)
                m_Array[i] = inArray[i, i];
        }
        #endregion

        private bool CheckDiagonal(T[,] inArray) {
            int size = inArray.GetLength(0);
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    if(i != j)
                        if(!inArray[i, j].Equals(default(T)))
                            return false;
                }
            }
            return true;
        }

        public override int GetHashCode() => m_Array?.GetHashCode() ?? 0;

        protected override T GetValue(int i, int j) => i == j ? m_Array[i] : default(T);

        protected override void SetValue(int i, int j, T value) {
            if(i == j) m_Array[i] = value;
            throw new ArgumentException("We try to set value on non-diagonal line");
        }
    }
}
