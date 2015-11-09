using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Matrix.Models {
    public class SquareMatrix<T> : IEquatable<SquareMatrix<T>> {

        public event EventHandler<MatrixEventArgs> Update = delegate { };

        protected T[,] array;
        public int Size { get; protected set; }

        #region Constructors
        public SquareMatrix(int size) {
            if(size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));
            Size = size;
            array = new T[Size, Size];
            for(int i = 0; i < Size; i++) {
                for(int j = 0; j < Size; j++)
                    array[i, j] = default(T);
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

        public void Accept(IMatrixVisitor<T> visitor, SquareMatrix<T> matrix) {
            visitor.Visit((dynamic)this, matrix);
        }

        public IEnumerable<T> GetMatrix() {
            for(int i = 0; i < Size; i++)
                for(int j = 0; j < Size; j++)
                    yield return this[i, j];
        }

        public bool Equals(SquareMatrix<T> other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if(Size != other.Size) return false;

            for(int i = 0; i < Size; i++)
                for(int j = 0; j < Size; j++)
                    if(!array[i, j].Equals(other.array[i, j]))
                        return false;

            return true;
        }

        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if(obj.GetType() != GetType() && !obj.GetType().IsSubclassOf(typeof(SquareMatrix<T>)))
                return false;
            return Equals((SquareMatrix<T>) obj);
        }

        public override string ToString() {
            StringBuilder matrixBuilder = new StringBuilder();
            matrixBuilder.Append($"Matrix. Size: {Size}\n");
            for(int i = 0; i < Size; i++) {
                for(int j = 0; j < Size; j++) {
                    matrixBuilder.Append($"{this[i, j], 5}");
                }
                matrixBuilder.Append("\n");
            }
            return matrixBuilder.ToString(); 
        }

        #endregion

        #region Protected Methods
        protected void InitializeMatrix(T[,] inArray) {
            array = new T[Size, Size];
            for (int i = 0; i < Size; i++) {
            for (int j = 0; j < Size; j++)
                array[i, j] = inArray[i, j];
            }
        }

        protected void OnUpdate(object sender, MatrixEventArgs e) {
            var update = Update;
            update?.Invoke(sender, e);
        }
        #endregion
    }

}
