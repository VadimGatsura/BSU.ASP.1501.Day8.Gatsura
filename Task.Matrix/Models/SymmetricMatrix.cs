using System;

namespace Task.Matrix.Models {
    public class SymmetricMatrix<T>: SquareMatrix<T> {
        #region Constructors
        public SymmetricMatrix(int size) : base(size) { }

        public SymmetricMatrix(T[,] inArray) : base(inArray.GetLength(0)) {
            if (inArray == null)
                throw new ArgumentNullException(nameof(inArray));
            if (inArray.GetLength(0) != inArray.GetLength(1) && inArray.GetLength(0) > 0)
                throw new ArgumentException($"Argument {nameof(inArray)} doesn't square array");
            if(!CheckSymmetry(inArray))
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
                array[i, j] = value;
                array[j, i] = value;
                OnUpdate(this , new MatrixEventArgs(i, j));
                OnUpdate(this, new MatrixEventArgs(j, i));
            }
        }

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
    }
}
