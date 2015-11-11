using System;
using Task.Matrix.Models;
using static System.Console;

namespace Task.Matrix.ConsoleUI {
    class Program {
        static void Main(string[] args) {
            MatrixListener listener = new MatrixListener();
            Matrix<int> a = new SquareMatrix<int>(new[,] { {0,1,2}, {3,4,5}, {6,7,8} });
            Matrix<int> b = new DiagonalMatrix<int>(new[,] { { 1, 0, 0 }, { 0, 2, 0 }, { 0, 0, 3 } });

            WriteLine($"Matrix a: {a}");
            WriteLine($"Matrix b: {b}");

            listener.Register(a);
            listener.Register(b);

            a[2, 1] = 5;
            b[2, 2] = 10;
            listener.Unregister(a);

            a[2, 1] = -5;
            b[2, 2] = -10;
            listener.Unregister(b);

            a[2, 1] = 50;
            b[2, 2] = 1;


            ReadKey();
        }
    }

    class MatrixListener {
        public void Register<T>(Matrix<T> matrix) {
            if(matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            matrix.Update += MatrixUpdate;
        }

        public void Unregister<T>(Matrix<T> matrix) {
            if(matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            matrix.Update -= MatrixUpdate;
        }

        public void MatrixUpdate(object sender, MatrixEventArgs e) {
            WriteLine("Matrix update");
            WriteLine($"i: {e.I}, j: {e.J}");
        }
    }
}
