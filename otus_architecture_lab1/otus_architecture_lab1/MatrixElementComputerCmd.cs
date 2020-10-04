using System;


namespace otus_architecture_lab1
{
    class MatrixElementComputerCmd : ICommand
    {
        #region Variables

        Action<bool, object> callback = null;

        Matrix matrixA;
        Matrix matrixB;

        int row, column;

        #endregion



        #region Class lifecycle

        public MatrixElementComputerCmd(Matrix matrixA, Matrix matrixB, int row, int column)
        {
            this.matrixA = matrixA;
            this.matrixB = matrixB;

            this.row = row;
            this.column = column;
        }

        #endregion



        #region Methods

        public void SetResultCallback(Action<bool, object> callback)
        {
            this.callback = callback;
        }


        public void Run()
        {
            float resultValue = 0.0f;

            for(int i = 0; i < matrixA.Columns; i++)
            {
                resultValue += matrixA[row, i] * matrixB[i, column];
            }

            callback?.Invoke(true, resultValue);
        }

        #endregion
    }
}
