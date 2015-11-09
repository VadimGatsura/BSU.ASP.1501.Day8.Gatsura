using System;

namespace Task.Matrix {
    public class MatrixEventArgs: EventArgs {

        public int I { get; }
        public int J { get; }

        public MatrixEventArgs(int i, int j) {
            I = i;
            J = j;
        }
    }
}
