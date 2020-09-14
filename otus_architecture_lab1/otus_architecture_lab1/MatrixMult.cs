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
            this.matrixA = matrixA;
            this.matrixB = matrixB;

            result = new Matrix();
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
            await Task.Delay(3000);
        }

        #endregion
    }
}
