using System;

namespace Task.Matrix.Models {
    public class DiagonalMatrix<T> : SquareMatrix<T> {
        #region Constructors
        public DiagonalMatrix(int size) : base(size) { }

        public DiagonalMatrix(T[,] inArray) : base(inArray.GetLength(0)) {
            if (inArray.GetLength(0) != inArray.GetLength(1) && inArray.GetLength(0) > 0)
                throw new ArgumentException($"Argument {nameof(inArray)} doesn't square array");
            if (!CheckDiagonal(inArray))
                throw new ArgumentException($"{nameof(inArray)} doesn't symmetric matrix");
            Size = inArray.GetLength(0);
            InitializeMatrix(inArray);
        }
        #endregion

        public override T this[int i, int j] {
            get { return base[i, j]; }
            set {
                if (i < 0 && i >= Size)
                    throw new ArgumentOutOfRangeException(nameof(i));
                if (j < 0 && j >= Size)
                    throw new ArgumentOutOfRangeException(nameof(j));
                if(i != j)
                    throw new ArgumentException("In diagonal matrix you may change elements only in diagonal");
                array[i, j] = value;
                OnUpdate(this, new MatrixEventArgs(i, j));
            }
        }

        private bool CheckDiagonal(T[,] inArray) {
            int size = inArray.GetLength(0);
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    if(i != j)
                        if(!array[i, j].Equals(default(T)))
                            return false;
                }
            }
            return true;
        }
    }
}
