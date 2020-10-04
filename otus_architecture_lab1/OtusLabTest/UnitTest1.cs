using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using otus_architecture_lab1;


namespace OtusLabTest
{
    [TestClass]
    public class MatrixTest
    {
        class LoggerMock : ILogger
        {
            public void Log(string message) { }
            public void Dispose() { }
        }


        [TestMethod]
        public void ReadWriteMatrixTest()
        {
            Matrix matrixOrigin = FetchMatrixA();

            new MatrixWriterTextFile("tmp_matrix.txt").Write(matrixOrigin);

            Matrix matrixReaded = new MatrixReaderTextFile("tmp_matrix.txt").Read();

            Assert.AreEqual(matrixOrigin.Rows, matrixReaded.Rows);
            Assert.AreEqual(matrixOrigin.Columns, matrixReaded.Columns);

            for (int i = 0; i < matrixOrigin.Rows; i++)
            {
                for (int j = 0; j < matrixOrigin.Columns; j++)
                {
                    Assert.AreEqual(matrixOrigin[i,j], matrixReaded[i,j], 0.001, "Values readed wrong");
                }
            }
        }


        [TestMethod]
        public void MullMatrixTest()
        {
            // TODO it's like integration test but not unit test, becouse we have to initialize singletones
            // it's should fixed
            SimpleServiceLocator.Instance.RegisterService<ILogger>(new LoggerMock());
            SimpleServiceLocator.Instance.RegisterService<ICommandExecutor>(new SequenceCommandExecutor());


            Matrix matrixA = FetchMatrixA();
            Matrix matrixB = FetchMatrixB();

            Matrix matrixExpected = FetchMatrixAMulB();

            ICommand mulCmd = new MatrixMultCommand(matrixA, matrixB);
            mulCmd.SetResultCallback((isSucced, result) =>
            {
                Matrix matrixActual = result as Matrix;

                Assert.AreEqual(matrixExpected.Rows, matrixActual.Rows);
                Assert.AreEqual(matrixExpected.Columns, matrixActual.Columns);

                for (int i = 0; i < matrixExpected.Rows; i++)
                {
                    for (int j = 0; j < matrixExpected.Columns; j++)
                    {
                        Assert.AreEqual(matrixExpected[i, j], matrixActual[i, j], 0.001, "Values computed wrong");
                    }
                }

            });

            mulCmd.Run();

            SimpleServiceLocator.Instance.Dispose();
        }


        private Matrix FetchMatrixA()
        {
            Matrix matrix = new Matrix(3, 3);

            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;

            matrix[1, 0] = 4;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;

            matrix[2, 0] = 7;
            matrix[2, 1] = 8;
            matrix[2, 2] = 9;

            return matrix;
        }


        private Matrix FetchMatrixB()
        {
            Matrix matrix = new Matrix(3, 3);

            matrix[0, 0] = 9;
            matrix[0, 1] = 8;
            matrix[0, 2] = 7;

            matrix[1, 0] = 6;
            matrix[1, 1] = 5;
            matrix[1, 2] = 4;

            matrix[2, 0] = 3;
            matrix[2, 1] = 2;
            matrix[2, 2] = 1;

            return matrix;
        }


        private Matrix FetchMatrixAMulB()
        {
            Matrix A = FetchMatrixA();
            Matrix B = FetchMatrixB();

            Matrix matrix = new Matrix(3, 3);

            matrix[0, 0] = A[0,0] * B[0,0] + A[0, 1] * B[1, 0] + A[0, 2] * B[2, 0];
            matrix[0, 1] = A[0, 0] * B[0, 1] + A[0, 1] * B[1, 1] + A[0, 2] * B[2, 1];
            matrix[0, 2] = A[0, 0] * B[0, 2] + A[0, 1] * B[1, 2] + A[0, 2] * B[2, 2];

            matrix[1, 0] = A[1, 0] * B[0, 0] + A[1, 1] * B[1, 0] + A[1, 2] * B[2, 0];
            matrix[1, 1] = A[1, 0] * B[0, 1] + A[1, 1] * B[1, 1] + A[1, 2] * B[2, 1];
            matrix[1, 2] = A[1, 0] * B[0, 2] + A[1, 1] * B[1, 2] + A[1, 2] * B[2, 2];

            matrix[2, 0] = A[2, 0] * B[0, 0] + A[2, 1] * B[1, 0] + A[2, 2] * B[2, 0];
            matrix[2, 1] = A[2, 0] * B[0, 1] + A[2, 1] * B[1, 1] + A[2, 2] * B[2, 1];
            matrix[2, 2] = A[2, 0] * B[0, 2] + A[2, 1] * B[1, 2] + A[2, 2] * B[2, 2];

            return matrix;
        }
    }
}
