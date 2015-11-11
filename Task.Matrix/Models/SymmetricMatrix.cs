using System;

namespace Task.Matrix.Models {
    public class SymmetricMatrix<T>: SquareMatrix<T> {
        private readonly T[][] m_Array;
        #region Constructors

        public SymmetricMatrix(int size) {
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));
            Size = size;
            m_Array = new T[Size][];
            for (int i = 0; i < Size; i++) {
                m_Array[i] = new T[i + 1];
                for (int j = 0; j < i + 1; j++)
                    m_Array[i][j] = default(T);
            }
        }

        public SymmetricMatrix(T[,] inArray)  {
            if (inArray == null)
                throw new ArgumentNullException(nameof(inArray));
            if (inArray.GetLength(0) != inArray.GetLength(1) && inArray.GetLength(0) > 0)
                throw new ArgumentException($"Argument {nameof(inArray)} doesn't square array");
            if(!CheckSymmetry(inArray))
                throw new ArgumentException($"{nameof(inArray)} doesn't symmetric matrix");
            Size = inArray.GetLength(0);
            m_Array = new T[Size][];
            for(int i = 0; i < Size; i++) {
                m_Array[i] = new T[i + 1];
                for(int j = 0; j < i + 1; j++)
                    m_Array[i][j] = inArray[i, j];
            }
        } 
        #endregion

        private bool CheckSymmetry(T[,] inArray) {
            int size = inArray.GetLength(0);
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    if(!inArray[i, j].Equals(inArray[j, i]))
                        return false;
                }
            }
            return true;
        }

        public override int GetHashCode() => m_Array?.GetHashCode() ?? 0;

        protected override T GetValue(int i, int j) => i >= j ? m_Array[i][j] : m_Array[j][i];

        protected override void SetValue(int i, int j, T value) {
            if(i >= j) m_Array[i][j] = value;
            else m_Array[j][i] = value;
        }
    }
}
