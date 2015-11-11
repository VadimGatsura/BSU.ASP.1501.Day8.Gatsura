using System;
using System.Text;

namespace Task.Matrix.Models {
    public abstract class Matrix<T>: IEquatable<Matrix<T>> {

        public event EventHandler<MatrixEventArgs> Update = delegate { };

        public int Size { get; protected set; }

        public T this[int i, int j] {
            get {
                if (i < 0 && i >= Size)
                    throw new ArgumentOutOfRangeException(nameof(i));
                if (j < 0 && j >= Size)
                    throw new ArgumentOutOfRangeException(nameof(j));
                return GetValue(i, j);
            }
            set {
                if (i < 0 && i >= Size)
                    throw new ArgumentOutOfRangeException(nameof(i));
                if (j < 0 && j >= Size)
                    throw new ArgumentOutOfRangeException(nameof(j));

                SetValue(i, j, value);
                OnUpdate(this, new MatrixEventArgs(i, j));
            }
        }
        public override abstract int GetHashCode();

        protected bool EqualsByIndexator(Matrix<T> other) {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    if (!this[i, j].Equals(other[i, j]))
                        return false;

            return true;
        }

        public void Accept(IMatrixVisitor<T> visitor, Matrix<T> matrix) {
            visitor.Visit((dynamic)this, matrix);
        }

        public bool Equals(Matrix<T> other) {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return Size == other.Size && EqualsByIndexator(other);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType() && !obj.GetType().IsSubclassOf(typeof(SquareMatrix<T>)))
                return false;
            return EqualsByIndexator((Matrix<T>)obj);
        }

        public override string ToString() {
            StringBuilder matrixBuilder = new StringBuilder();
            matrixBuilder.Append($"Matrix. Size: {Size}\n");
            for (int i = 0; i < Size; i++) {
                for (int j = 0; j < Size; j++) {
                    matrixBuilder.Append($"{this[i, j],5}");
                }
                matrixBuilder.Append("\n");
            }
            return matrixBuilder.ToString();
        }

        protected abstract T GetValue(int i, int j);
        protected abstract void SetValue(int i, int j, T value);

        protected void OnUpdate(object sender, MatrixEventArgs e) {
            var update = Update;
            update?.Invoke(sender, e);
        }
    }
}