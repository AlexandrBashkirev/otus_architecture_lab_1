using System;
using System.Collections.Generic;


namespace otus_architecture_lab1
{
    public class MatrixMultCommand : AsyncCommand
    {
        #region Variables

        Matrix result = null;
        Matrix matrixA = null;
        Matrix matrixB = null;

        #endregion



        #region Class lifecycle

        public MatrixMultCommand(Matrix matrixA, Matrix matrixB)
        {
            if(matrixA.Columns != matrixB.Rows)
            {
                throw new Exception("Can't multiplay matrix");
            }

            this.matrixA = matrixA;
            this.matrixB = matrixB;

            result = new Matrix(matrixA.Rows, matrixB.Columns);
        }

        #endregion



        #region Methods

        public override void Run()
        {
            SimpleServiceLocator.Instance.GetService<ICommandExecutor>().Execute(MultiplayCommands());
        }


        private IEnumerable<ICommand> MultiplayCommands()
        {
            int cmdCount = result.Rows * result.Columns;
            object lockObj = new object();

            for (int row = 0; row < result.Rows; row ++)
            {
                for (int column = 0; column < result.Columns; column++)
                {
                    ICommand cmd = new MatrixElementComputeCmd(matrixA, matrixB, row, column);
                    int _row = row;
                    int _column = column;

                    cmd.SetResultCallback((isSuccess, value) =>
                    {
                        result[_row, _column] = (float)value;
                        lock (lockObj)
                        {
                            cmdCount--;
                        }

                        if(cmdCount == 0)
                        {
                            IsDone = true;
                            callback?.Invoke(true, result);
                        }
                    });

                    yield return cmd;
                }
            }
        }

        #endregion
    }
}
