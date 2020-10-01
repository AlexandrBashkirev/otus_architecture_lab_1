using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace otus_architecture_lab1
{
    class MatrixMult
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

        public bool IsDone { get; private set; }

        public Matrix Result => result;

        #endregion



        #region Methods

        public void Initialize()
        {

        }


        public async Task Solve()
        {
            SimpleServiceLocator.Instance.GetService<ICommandExecutor>().Execute(MultiplayCommands());
            await Task.Delay(1);
        }


        private IEnumerable<ICommand> MultiplayCommands()
        {
            for(int row = 0; row < result.Rows; row ++)
            {
                for (int column = 0; column < result.Columns; column++)
                {
                    yield return new MatrixElementComputerCmd(matrixA, matrixB, result,  row, column);
                }
            }
        }

        #endregion
    }
}
