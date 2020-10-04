using System.Threading;

namespace otus_architecture_lab1
{
    class MatrixElementComputeCmd : CommandBase
    {
        #region Variables

        Matrix matrixA;
        Matrix matrixB;

        int row, column;

        #endregion



        #region Class lifecycle

        public MatrixElementComputeCmd(Matrix matrixA, Matrix matrixB, int row, int column)
        {
            this.matrixA = matrixA;
            this.matrixB = matrixB;

            this.row = row;
            this.column = column;
        }

        #endregion



        #region Methods

        public override void Run()
        {
            float resultValue = 0.0f;

            for(int i = 0; i < matrixA.Columns; i++)
            {
                resultValue += matrixA[row, i] * matrixB[i, column];
            }

            SimpleServiceLocator.Instance.GetService<ILogger>()
                .Log($"Element [{row},{column}] computed in thread {Thread.CurrentThread.Name}");

            callback?.Invoke(true, resultValue);
        }

        #endregion
    }
}
