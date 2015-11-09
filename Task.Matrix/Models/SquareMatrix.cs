using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Matrix.Models {
    public class SquareMatrix<T> : Matrix<T> {
        protected T[,] array;
        public int Size { get; protected set; }

        #region Constructors
        public SquareMatrix(): this(1) {} 
        public SquareMatrix(int size) {
            if(size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));
            Size = size;
            array = new T[Size, Size];
            for(int i = 0; i < Size; i++) {
                for(int j = 0; j < Size; j++)
                    this[i, j] = default(T);
            }

        }

        public SquareMatrix(T[,] inArray) {
            if(inArray == null)
                throw new ArgumentNullException(nameof(inArray));
            if(inArray.GetLength(0) != inArray.GetLength(1) && inArray.GetLength(0) > 0)
                throw new ArgumentException($"Argument {nameof(inArray)} doesn't square array");

            Size = inArray.GetLength(0);
            InitializeMatrix(inArray);
        } 
        #endregion

        public virtual T this[int i, int j] {
            get {
                if( i < 0 && i >= Size)
                    throw new ArgumentOutOfRangeException(nameof(i));
                if (j < 0 && j >= Size)
                    throw new ArgumentOutOfRangeException(nameof(j));
                return array[i, j];
            }
            set {
                if (i < 0 && i >= Size)
                    throw new ArgumentOutOfRangeException(nameof(i));
                if (j < 0 && j >= Size)
                    throw new ArgumentOutOfRangeException(nameof(j));
                array[i, j] = value;
                OnUpdate(this, new MatrixEventArgs(i, j));
            }
        }

        #region Public Methods

        public IEnumerable<T> GetMatrix() {
            for(int i = 0; i < Size; i++)
                for(int j = 0; j < Size; j++)
                    yield return this[i, j];
        }

        public override string ToString() {
            StringBuilder matrixBuilder = new StringBuilder();
            matrixBuilder.Append($"Matrix. Size: {Size}");
            for(int i = 0; i < Size; i++) {
                for(int j = 0; j < Size; j++) {
                    matrixBuilder.Append($"{this[i, j], 10}");
                }
                matrixBuilder.Append("\n");
            }
            return matrixBuilder.ToString(); 
        }

        #endregion

        protected void InitializeMatrix(T[,] inArray) {
            array = new T[Size, Size];
            for (int i = 0; i < Size; i++) {
            for (int j = 0; j < Size; j++)
                this[i, j] = inArray[i, j];
            }
        }
    }

}
