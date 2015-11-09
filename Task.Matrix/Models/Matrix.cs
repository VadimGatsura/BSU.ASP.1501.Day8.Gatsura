using System;

namespace Task.Matrix.Models {
    public class Matrix<T> {

        public event EventHandler<MatrixEventArgs> Update = delegate {};

        #region Public Methods
        public void Accept(IMatrixVisitor<T> visitor, Matrix<T> matrix) {
            visitor.Visit((dynamic) this, matrix);
        }
        #endregion

        #region Private Methods

        protected void OnUpdate(object sender, MatrixEventArgs e) {
            var update = Update;
            update?.Invoke(sender, e);
        }
        #endregion
    }
}
