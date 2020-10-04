using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace otus_architecture_lab1
{
    class MatrixMult : AsyncCommand
    {
        #region Variables

        Matrix result = null;
        Matrix matrixA = null;
        Matrix matrixB = null;

        #endregion



        #region Class lifecycle

        public MatrixMult(Matrix matrixA, Matrix matrixB)
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



        #region Properties

        public Matrix Result => result;

        #endregion



        #region Methods

        public override void Run()
        {
            SimpleServiceLocator.Instance.GetService<ICommandExecutor>().Execute(MultiplayCommands());
        }


        private IEnumerable<ICommand> MultiplayCommands()
        {
            int cmdCount = result.Rows * result.Columns;

            for (int row = 0; row < result.Rows; row ++)
            {
                for (int column = 0; column < result.Columns; column++)
                {
                    ICommand cmd = new MatrixElementComputerCmd(matrixA, matrixB, row, column);

                    cmd.SetResultCallback((isSuccess, value) =>
                    {
                        result[row, column] = (float)value;
                        cmdCount--;

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
