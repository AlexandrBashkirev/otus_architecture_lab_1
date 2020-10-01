
namespace otus_architecture_lab1
{
    class MatrixElementComputerCmd : ICommand
    {
        #region Variables

        Matrix matrixA;
        Matrix matrixB;

        Matrix result;

        int row, column;

        #endregion



        #region Class lifecycle

        public MatrixElementComputerCmd(Matrix matrixA, Matrix matrixB, Matrix result, int row, int column)
        {
            this.matrixA = matrixA;
            this.matrixB = matrixB;

            this.result = result;

            this.row = row;
            this.column = column;
        }

        #endregion


        public void Run()
        {
            float resultValue = 0.0f;

            for(int i = 0; i < matrixA.Columns; i++)
            {
                resultValue += matrixA[row, i] * matrixB[i, column];
            }

            result[row, column] = resultValue;
        }
    }
}
